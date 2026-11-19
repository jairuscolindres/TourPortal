# Operational Systems Proposal — TourPortal

This document addresses the payment, verification, and operational gaps in the current prototype and proposes the agreed operational model for the next phase of development.

---

## 1. The Core Problem

The current app implies payment has occurred (`TotalPaid: $248 USD`) but:
- No payment gateway exists
- No booking is stored anywhere
- No guide or receptionist has any system to check against
- The QR code is decorative

This means there is currently **no operational loop** — the user journey ends at the pass page with no way for anyone on the ground to act on it.

---

## 2. Agreed Decisions

| Topic | Decision | Rationale |
|---|---|---|
| Payment model | **Pay at reception on arrival** | Zero cost, no payment gateway needed |
| Email | **Resend** (free tier — 3,000/month, 100/day) | Solves proof-of-booking; user receives reference code in inbox |
| Database | **Supabase** via `dotnet add package Supabase` | Free tier, no infrastructure cost |
| Seat holding | **Reservations hold a seat** | Simpler to implement — one count query, no payment status filtering |
| Guide tooling | **Physical printed sheet** handed out by receptionist | No app, no tech dependency on the day |
| Receptionist tooling | **Dashboard at a separate URL** on the same website | No separate deployment needed |

---

## 3. Agreed Operational Flow

```
1. User browses tours on the website
2. User fills out booking form and clicks "Generate Pass"
      → Booking is saved to Supabase (status: reserved)
      → Booking counts against tour capacity immediately
      → Confirmation email sent via Resend with reference code + pass link
3. User arrives at the physical office
      → Shows reference code (from email or screenshot of pass)
4. Receptionist looks up the booking in the dashboard (/reception)
      → Confirms traveler identity
      → Collects payment (cash or card at desk)
      → Marks booking as paid in the dashboard
      → Prints the physical guide sheet for the traveler and the tour guide
5. User and tour guide each receive a printed copy of the guide sheet
```

---

## 4. Capacity Model

Each tour departure has a `MaxGroupSize` (e.g. 8 travelers).

**Rule:** A reservation claims a seat the moment it is submitted — regardless of payment status.

**Implementation:** To check available capacity, count all bookings for a given tour and departure date, compare to `MaxGroupSize`.

```
availableSeats = tour.MaxGroupSize - confirmedBookings.Count(tourId, departureDate)
if (availableSeats <= 0) → tour is full, booking rejected
```

The receptionist can manually cancel no-show reservations to free seats back up.

---

## 5. Receptionist Dashboard (`/reception`)

A separate URL on the same Blazor app. No separate deployment. Access should be restricted (simple password or IP-based for now).

**Key features:**
- **Search bookings** — by reference code, name, or phone number
- **Today's tours** — list of all departures today with traveler counts (`4 / 8`)
- **Booking detail** — traveler name, email, phone, guest count, tour, dates, payment status
- **Mark as paid** — receptionist records payment method (cash / card) and amount collected
- **Mark as checked in** — confirms traveler has arrived
- **Print guide sheet** — triggers print of the physical itinerary sheet for traveler and guide

---

## 6. Email — Resend Integration

Library: `Resend.Net` or direct HTTP to Resend API  
Free tier: 3,000 emails/month, 100/day — sufficient for prototype volume

**Emails sent:**
- **Booking confirmation** — sent on pass generation, contains reference code and a link to retrieve the pass at any time

**Emails not needed (kept simple):**
- No reminder emails (yet)
- No cancellation emails (yet)
- No post-tour review requests (yet)

---

## 7. Database — Supabase

Package: `dotnet add package Supabase`  
Free tier: 500MB database, 50,000 monthly active users

**Bookings table (minimum viable schema):**

| Column | Type | Notes |
|---|---|---|
| `id` | uuid | Primary key |
| `reference_code` | text | Unique, e.g. `TP-4521-X` |
| `tour_id` | text | FK to tour catalog |
| `travel_dates` | text | Selected departure window |
| `lead_traveler_name` | text | |
| `email` | text | |
| `phone` | text | |
| `guest_count` | int | |
| `price_per_person` | numeric | |
| `total_price` | numeric | Calculated at booking time |
| `payment_status` | text | `unpaid` / `paid` |
| `payment_method` | text | `cash` / `card` — set by receptionist |
| `checked_in` | bool | Set by receptionist on arrival |
| `created_at` | timestamptz | Auto |

> [!NOTE]
> Tour data remains hardcoded in `TourDataService` for now. Only bookings are persisted to Supabase. Tour catalog migration to the database is a future concern.

---

## 8. Build Order (Next Phase)

```
Step 1 — Supabase integration
  Add Supabase package
  Create bookings table
  Wire up BookingStateService to persist on CreateBooking()

Step 2 — Pass retrieval
  Save reference code to DB on confirmation
  Add /booking/lookup page — user enters reference + email → retrieves pass
  Update BookingPass.razor to load from DB if in-memory state is empty

Step 3 — Resend email
  Send confirmation email on booking creation
  Email contains: reference code, tour title, dates, guest count, pass link

Step 4 — Real QR code
  Encode /booking/lookup?ref={referenceCode} into the QR SVG
  Replace static SVG with dynamically generated QR

Step 5 — Receptionist dashboard (/reception)
  Booking search (by ref, name, phone)
  Today's tours view with capacity indicators
  Mark paid + mark checked in actions
  Print guide sheet trigger

Step 6 — Regional Offices & Meetup Locations (TP-008)
  Seed regional office data (Bali, Kyoto, Dolomites)
  Surface office details on pass and printed guide sheet
  Scope receptionist view to office

Step 7 — Admin Dashboard & Tour Catalog CRUD (TP-009)
  Create tours, offices, and tour_departures tables in Supabase
  Migrate TourDataService from in-memory to Supabase
  Build /admin dashboard for tour, office, and departure slot CRUD
```

---

## 9. Deferred Ideas (Out of Scope for Prototype)

These were identified as valuable but are not being built now:

- Online payment gateway (Stripe etc.)
- Pre-tour reminder emails
- Day-of SMS to traveler
- Post-tour review collection
- Guide rating system
- Waitlist for full tours
- Traveler special notes at booking
- Split payment for groups
- Guide mobile verification app
- PWA offline pass
- Full itinerary timeline drag-and-drop CRUD (seed via SQL/migrations instead)
- Accessibility and language flags
