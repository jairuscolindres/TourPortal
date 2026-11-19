# TP-007 · Receptionist dashboard — no tool to manage bookings on the ground

**Priority:** High  
**Type:** Missing Feature

## Context
Payment is collected at the physical office (not online). The receptionist needs a way to look up reservations, confirm payment, and check travelers in. Without this, there is no operational loop — bookings exist in Supabase but no one on the ground can act on them.

## Route
`/reception` or `/reception/{officeSlug}` — a dedicated route on the same Blazor app. No separate deployment needed.

## Access Control — No User Accounts (Branch Station PIN)
Strict requirement: **Zero user accounts**. The system does not manage usernames, individual staff accounts, or user tables. Instead, authentication is based on a physical **Branch Station PIN**:
1. When opening `/reception`, if no office is unlocked in the session, the receptionist sees:
   - Select Office: `[TourPortal Bali ▾]` (or pre-selected if accessing `/reception/bali`)
   - Enter Office PIN: `[ • • • • ]`
2. PINs are verified against configuration in `appsettings.json` (e.g., `Offices:Bali:Pin = "2480"`).
3. Upon entering the correct PIN, the session unlocks that specific branch station.
4. "Lock Station" button clears the session and returns to the PIN prompt.

## Branch Scoping — Office-Specific Access
To prevent human error and confusion between different physical offices worldwide:
- **Strictly scoped to the unlocked office**: The receptionist only sees tours, departure slots, and bookings associated with their branch (e.g., Bali desk only accesses Bali tours).
- Booking searches default to the local office (with an optional toggle to search global if a traveler booked the wrong region by mistake).

## Features

### Booking search
- Search by reference code, lead traveler name, or phone number within the active branch
- Returns matching booking(s) with full detail

### Today's tours view
- Lists all tour departures scheduled for today at this branch
- Each tour shows traveler list and capacity indicator (e.g. `4 / 8 reserved`)
- Highlights unpaid and unchecked-in bookings

### Booking detail view
- Lead traveler name, email, phone
- Tour title, travel dates, guest count
- Total price due
- Current `payment_status` and `checked_in` state

### Mark as paid
- Receptionist selects payment method (cash / card) and confirms
- Updates `payment_status` → `paid` and `payment_method` in Supabase

### Mark as checked in
- Receptionist confirms traveler has arrived
- Updates `checked_in` → `true` in Supabase

### Print guide sheet
- Triggers a print-optimised layout of the tour itinerary for the traveler and guide
- Includes: branch office header/address, tour title, dates, traveler name, guest count, reference code, full day-by-day itinerary, meeting point

## Acceptance criteria
- [ ] Navigating to `/reception` displays the Office Selector & PIN prompt if not unlocked
- [ ] Correct PIN unlocks the dashboard for that branch; invalid PIN shows error
- [ ] Dashboard strictly filters today's tours and departures to the unlocked office
- [ ] Booking search returns results filtered to the active branch
- [ ] Today's tours view loads bookings for today's departure dates for that branch
- [ ] Capacity indicator reflects current reservation count vs `Tour.MaxGroupSize`
- [ ] Mark as paid updates `payment_status` and `payment_method` in Supabase
- [ ] Mark as checked in updates `checked_in` in Supabase
- [ ] Print guide sheet renders clean print layout with branch office details and triggers `window.print()`
- [ ] "Lock Station" button locks the dashboard and clears the unlocked office state

## Dependencies
- Requires TP-002 (Supabase booking storage)
- Requires TP-008 (Office location model & data)
