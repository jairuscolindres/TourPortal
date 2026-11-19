# TP-001 · Pass retrieval — users lose their pass if they close the tab

**Priority:** High  
**Type:** Bug / Missing Feature

## Problem
Booking state is held in scoped in-memory `BookingStateService`. Navigating away or closing the tab destroys the session, making the pass permanently unrecoverable. There is no way for a user to retrieve their pass after the fact.

## Expected behaviour
A user should be able to return to their pass at any time using their booking reference and email address. The reference code is delivered via confirmation email (TP-003), so the user always has it in their inbox.

## Acceptance criteria
- [ ] Add a `/booking/lookup` page with a simple form (reference code + email)
- [ ] On submit, query Supabase `bookings` table for a matching record
- [ ] If found, re-render the pass page identically to the original confirmation
- [ ] If not found, show a clear error message
- [ ] `BookingPass.razor` falls back to a Supabase lookup if `BookingStateService.CurrentBooking` is the default (i.e. session was lost)

## Dependencies
Requires TP-002 (Supabase booking storage) to be in place first.
