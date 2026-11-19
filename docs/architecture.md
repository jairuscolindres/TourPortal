# Architecture — TourPortal

## Overview

TourPortal is a **Blazor Server** web application (.NET 8) for browsing and booking small-group tours. It has no external database, no authentication system, and no third-party APIs. All data is in-memory and all rendering happens server-side over a persistent SignalR connection.

---

## Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 / Blazor Server |
| Rendering | Interactive Server (`@rendermode InteractiveServer`) |
| Styling | Tailwind CSS (via `tailwind.config.js` + `wwwroot/css/app.css`) |
| Tour data | In-memory (hardcoded seed data in `TourDataService`) |
| Booking storage | **Supabase** (`dotnet add package Supabase`) — free tier |
| Session state | Scoped in-memory `BookingStateService` (per Blazor circuit) |
| Email | **Resend** (free tier — 3,000/month, 100/day) |
| Payment | None — collected at reception in person |
| Auth | **Zero user accounts** — Master Passcode for `/admin`, Branch Station PINs for `/reception` |

---

## Project Structure

```
TourPortal/
├── Components/
│   ├── App.razor               # Root component
│   ├── Routes.razor            # Route declarations
│   ├── _Imports.razor          # Global using directives
│   ├── Layout/
│   │   ├── MainLayout.razor    # Shell layout (nav, page slot)
│   │   └── NavMenu.razor       # Top navigation bar
│   └── Pages/
│       ├── Home.razor          # / — Explore hub
│       ├── TourDetails.razor   # /tours/{id} — Tour detail + booking form
│       ├── BookingPass.razor   # /booking/pass — Digital tour pass
│       ├── BookingLookup.razor # /booking/lookup — Pass retrieval (Planned - TP-001)
│       ├── Reception.razor     # /reception — Desk check-in dashboard (Planned - TP-007)
│       ├── Admin.razor         # /admin — Tour/office CRUD dashboard (Planned - TP-009)
│       └── Error.razor         # Error boundary page
├── Models/
│   ├── Tour.cs                 # Tour, TourItineraryDay, TourItineraryItem
│   ├── GuestBooking.cs         # Booking record (traveler details, ref code)
│   └── GuestTraveler.cs
├── Services/
│   ├── ITourDataService.cs     # Interface for tour data
│   ├── TourDataService.cs      # Singleton — hardcoded in-memory tour catalog
│   └── BookingStateService.cs  # Scoped — holds active booking for a session
├── database/
│   ├── schema.sql              # Supabase PostgreSQL DDL, indexes, RLS policies
│   └── seed.sql                # Initial test rows for tours and bookings (TP-002.5)
├── wwwroot/                    # Static assets (CSS, favicon)
├── Program.cs                  # App bootstrap & DI registration
└── docs/                       # Project documentation
```

---

## Data Flow

### Tour browsing
```
User browser
  → Blazor Server (SignalR)
    → Home.razor
      → ITourDataService.GetAllTours()   # reads singleton in-memory list
        → filtered/sorted in component
          → renders tour cards
```

### Booking
```
User fills form on TourDetails.razor
  → clicks "Confirm & Generate Pass"
    → HandleConfirmBooking()
      → BookingStateService.CreateBooking(booking)
        → assigns reference code (random 4-digit, e.g. TP-8942-X)
        → stores booking in CurrentBooking (scoped, in-memory)
        → NavigationManager.NavigateTo("/booking/pass")
          → BookingPass.razor reads BookingState.CurrentBooking
            → renders digital pass
```

---

## Service Registration

Defined in [`Program.cs`](../Program.cs):

```csharp
// Singleton: one shared instance, tour data never changes at runtime
builder.Services.AddSingleton<ITourDataService, TourDataService>();

// Scoped: one instance per Blazor circuit (i.e. per browser tab/session)
builder.Services.AddScoped<BookingStateService>();
```

> [!WARNING]
> `BookingStateService` is scoped to the Blazor circuit lifetime. If the user closes the tab, refreshes, or the server restarts, `CurrentBooking` is reset to the hardcoded default (`TP-8942-X / Alex Morgan`). There is no persistence layer. See [TP-001](../tickets/TP-001-pass-retrieval.md) and [TP-002](../tickets/TP-002-persistent-booking-storage.md).

---

## Models

### `Tour`
Represents a single tour in the catalog.

| Field | Type | Notes |
|---|---|---|
| `Id` | `string` | Slug, e.g. `tour-ubud-naturally` |
| `Title` | `string` | Display name |
| `Location` / `Country` | `string` | Used for filtering and display |
| `Category` | `string` | `Nature`, `Beach`, `Culture`, `Eco` |
| `Price` | `decimal` | Per person (USD) |
| `Rating` | `double` | Used for sorting and display |
| `GalleryImages` | `List<string>` | Unsplash URLs |
| `Inclusions` | `List<string>` | What's covered |
| `ItineraryDays` | `List<TourItineraryDay>` | Day-by-day schedule |

### `TourOffice`
Represents a physical regional office / branch associated with a tour.

| Field | Type | Notes |
|---|---|---|
| `Name` | `string` | e.g. `TourPortal Bali` |
| `Address` | `string` | Street address |
| `City` | `string` | City and postal code |
| `OpeningHours` | `string` | e.g. `Mon–Sat · 08:00–17:00 WITA` |
| `Phone` | `string` | Contact phone number |

### `GuestBooking`
Represents a confirmed booking. Created in `TourDetails.razor`, held in `BookingStateService`, consumed by `BookingPass.razor`.

| Field | Type | Notes |
|---|---|---|
| `ReferenceCode` | `string` | Generated at confirmation, e.g. `TP-4521-X` |
| `LeadTravelerName` | `string` | From booking form |
| `Email` | `string` | From booking form (no email is sent) |
| `GuestCount` | `int` | 1–8 |
| `TotalPaid` | `decimal` | `PricePerPerson × GuestCount` |
| `TourId` | `string` | Links back to `Tour.Id` |

---

## Pages & Routes

| Route | Component | Description |
|---|---|---|
| `/` | `Home.razor` | Explore hub — search, filter, tour grid |
| `/tours/{id}` | `TourDetails.razor` | Tour detail, photo gallery, booking form |
| `/booking/pass` | `BookingPass.razor` | Digital pass, itinerary timeline, print |
| `/booking/lookup` | *(Planned - TP-001)* | Pass retrieval via reference code + email |
| `/reception` | *(Planned - TP-007)* | Receptionist check-in, payment collection & guide sheet print (Station PIN gate, strictly office-scoped) |
| `/admin` | *(Planned - TP-009)* | Tour, office, and departure date CRUD dashboard (Master Passcode gate) |

---

## Security & Access Control (Zero User Accounts)

The application intentionally operates with **zero individual user accounts**, eliminating login tables, password resets, and user management overhead:

1. **Admin Access (`/admin`)**:
   - Protected by a **Master Passcode** stored in configuration (`Admin:Passcode`).
   - Navigating to `/admin` checks the browser session. If locked, prompts for the passcode.
   - Unlocked sessions can manage all tours, offices, and departures. Includes a "Lock Admin" action.
2. **Receptionist Access (`/reception`)**:
   - Protected by physical **Branch Station PINs** stored in configuration (e.g. `Offices:Bali:Pin`).
   - The receptionist selects their physical branch desk (or opens `/reception/{office}`) and enters the 4-digit PIN.
   - **Office Scoping**: Once unlocked, the dashboard strictly isolates data to that branch. Bali staff only see Bali tours, today's departures, and local bookings. Kyoto and Dolomites data are hidden to prevent operational cross-talk. Includes a "Lock Station" action.

---

## Roadmap & Tickets

> See the full visual tracker and dependency graph in [**Ticket Tracker (`tickets/README.md`)**](../tickets/README.md).

| Ticket | Description | Priority |
|---|---|---|
| [TP-001](../tickets/TP-001-pass-retrieval.md) | Pass retrieval page (`/booking/lookup`) | 🔴 High |
| [TP-002](../tickets/TP-002-persistent-booking-storage.md) | Persistent Supabase booking storage | 🔴 High |
| [TP-002.5](../tickets/TP-002.5-seed-data.md) | Seed initial data and test API connection | 🔴 High |
| [TP-003](../tickets/TP-003-confirmation-email.md) | Confirmation email dispatch via Resend | 🔴 High |
| [TP-004](../tickets/TP-004-qr-code-placeholder.md) | Dynamic scannable QR code generation | 🟡 Medium |
| [TP-005](../tickets/TP-005-cancellation-flow.md) | Cancellation / modification flow (deferred) | 🟢 Low |
| [TP-006](../tickets/TP-006-post-booking-guidance.md) | Post-booking arrival guidance & office notice | 🟢 Low |
| [TP-007](../tickets/TP-007-receptionist-dashboard.md) | Receptionist check-in & desk dashboard (`/reception`) | 🔴 High |
| [TP-008](../tickets/TP-008-office-location.md) | Regional offices & meetup points per tour collection | 🟡 Medium |
| [TP-009](../tickets/TP-009-admin-dashboard.md) | Admin dashboard (`/admin`) & catalog CRUD | 🟡 Medium |
