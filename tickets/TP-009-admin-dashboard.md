# TP-009 · Admin dashboard — no way to manage tours, offices, or departures

**Priority:** Medium  
**Type:** Feature

## Context
Tours, offices, and departure dates are currently hardcoded in `TourDataService` as in-memory seed data. The only way to add a new tour or update an existing one is to edit the C# source code and redeploy. An admin dashboard at `/admin` would allow non-technical staff to manage the catalog without touching code.

## Prerequisite — Tours must move to Supabase

Before any admin CRUD can exist, the tour catalog needs to be persisted in Supabase. `TourDataService` currently reads from a hardcoded in-memory list. This needs to change to read from a Supabase `tours` table, with the existing 4 tours seeded as initial data.

This is a meaningful architectural change and should be treated as the first step of this ticket.

### Supabase table: `tours`

| Column | Type | Notes |
|---|---|---|
| `id` | text | Slug, e.g. `tour-ubud-naturally` |
| `title` | text | |
| `location` | text | |
| `country` | text | |
| `category` | text | `Nature` / `Beach` / `Culture` / `Eco` |
| `price` | numeric | Per person (USD) |
| `rating` | numeric | Manually set for now |
| `review_count` | int | Manually set for now |
| `duration` | text | e.g. `3 days · 2 nights` |
| `image_url` | text | Unsplash URL or uploaded image URL |
| `gallery_images` | text[] | Array of image URLs |
| `description` | text | |
| `sub_category` | text | |
| `collection` | text | |
| `group_size` | int | Max travelers per departure |
| `guide_info` | text | |
| `leader_info` | text | |
| `inclusions` | text[] | Array of inclusion strings |
| `is_active` | bool | Controls visibility on the public site |
| `office_id` | uuid | FK to `offices` table |
| `created_at` | timestamptz | Auto |

### Supabase table: `offices`

| Column | Type | Notes |
|---|---|---|
| `id` | uuid | Primary key |
| `name` | text | e.g. `TourPortal Bali` |
| `address` | text | |
| `city` | text | |
| `opening_hours` | text | |
| `phone` | text | |

### Supabase table: `tour_departures`

Currently departure dates are a single text field on the tour (`AvailableDates`). Moving them to a proper table enables capacity tracking per departure slot.

| Column | Type | Notes |
|---|---|---|
| `id` | uuid | Primary key |
| `tour_id` | text | FK to `tours` |
| `label` | text | e.g. `12–14 Jun 2027` |
| `departure_date` | date | Start date |
| `max_group_size` | int | Can override tour default |
| `is_active` | bool | Admin can close a slot |
| `created_at` | timestamptz | Auto |

### Supabase table: `tour_itinerary_days` and `tour_itinerary_items`

For prototype, itinerary days can remain hardcoded or seeded via SQL. Full itinerary CRUD in the admin is a stretch goal (see below).

---

## Route
`/admin` — separate URL on the same Blazor app, alongside `/reception`.

## Access control — Zero User Accounts (Master Passcode Gate)
Strict requirement: **No user accounts, logins, or database user tables**.
- A single master secret passcode is configured in `appsettings.json` (e.g. `Admin:Passcode = "tour-admin-8842"`).
- Navigating to `/admin` checks if the session is unlocked:
  - **Locked**: Renders an unlock screen with a passcode prompt (`[ Enter Admin Passcode ]` + `Unlock` button).
  - **Unlocked**: Renders the full admin dashboard.
- Submitting the correct passcode unlocks the dashboard for the browser session.
- A "Lock Admin" button in the admin header immediately clears the unlocked state and returns to the gate.

---

## Admin CRUD — Tours

### List view
- Table of all tours (title, category, location, price, status active/inactive)
- Filter by category, office, active status
- "Add new tour" button

### Create / Edit tour form
- All fields from `tours` table
- Image URL input (no file upload for prototype — paste Unsplash or any URL)
- Gallery image URLs (add/remove rows)
- Inclusions (add/remove rows)
- Assigned office (dropdown from `offices` table)
- Active/inactive toggle

### Delete / Deactivate
- Deactivate removes tour from public listing without deleting the record
- Hard delete only if no bookings exist for that tour

---

## Admin CRUD — Departure dates

- Per-tour: list of departure slots with label, date, capacity, booking count, active status
- Add new departure slot (label, date, optional capacity override)
- Close a departure slot (marks `is_active = false` — slot no longer bookable)
- Cannot delete a departure with existing bookings

---

## Admin CRUD — Offices

- List of all offices
- Create new office (name, address, city, hours, phone)
- Edit office details
- Deactivate office (hides from tour assignment dropdown)

---

## Admin — Bookings overview (read + limited write)

The receptionist dashboard (TP-007) handles day-to-day check-in. The admin dashboard adds:
- All bookings across all tours, all dates
- Filter by tour, office, date range, payment status
- View booking detail
- Cancel a booking (frees the seat, marks `payment_status = cancelled`)

---

## Itinerary management (stretch goal — not required for prototype)

Managing itinerary days and items via a UI is complex. For the prototype, itinerary data can be:
- Seeded directly into Supabase via SQL insert
- Or remain hardcoded in a migration seed file

Full itinerary CRUD (add/edit/remove days and time-slot items) is deferred post-prototype.

---

## Acceptance criteria

### Prerequisite
- [ ] `tours`, `offices`, `tour_departures` tables created in Supabase
- [ ] Existing 4 tours seeded into `tours` table
- [ ] `TourDataService` updated to read from Supabase instead of in-memory list
- [ ] `ITourDataService` interface unchanged (rest of app unaffected)

### Admin dashboard
- [ ] Navigating to `/admin` shows Passcode Gate prompt if not unlocked
- [ ] Submitting correct passcode unlocks the admin dashboard for the session; incorrect passcode displays error
- [ ] "Lock Admin" button clears unlock state and relocks the dashboard
- [ ] Tours list view with status indicator
- [ ] Create tour form — saves to Supabase, appears on public site immediately
- [ ] Edit tour form — updates existing record
- [ ] Deactivate tour — removes from public listing
- [ ] Departure dates list per tour — add, close slots
- [ ] Offices list — create, edit, deactivate
- [ ] Bookings overview with cancel action

## Dependencies
- TP-002 (Supabase integration) must be in place first
- TP-008 (office model) informs the `offices` table schema
