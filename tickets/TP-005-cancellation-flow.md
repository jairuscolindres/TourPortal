# TP-005 · No cancellation or modification flow

**Priority:** Low  
**Status:** Deferred — out of scope for prototype

## Decision
Cancellations and modifications are handled manually by the receptionist directly in the Supabase dashboard for now. No user-facing cancellation flow will be built in the prototype phase.

## Deferred to
Post-prototype, once the receptionist dashboard (TP-007) is stable and volume justifies it.

## Original problem
Once a booking is confirmed, there is no way for a user to cancel, modify dates, or change guest count. There is also no policy communicated to the user about whether changes are possible.

## Future acceptance criteria (not being built now)
- [ ] Pass page or lookup page includes a "Cancel booking" action
- [ ] Cancellation flow confirms intent before acting
- [ ] Cancelled bookings free up their seat in capacity count
- [ ] Booking terms / cancellation policy is surfaced clearly
