# TP-003 · Confirmation email not actually sent

**Priority:** High  
**Type:** Missing Feature

## Decision
Use **Resend** for transactional email (free tier: 3,000/month, 100/day).  
Config keys live in `appsettings.Development.json` under `Resend:ApiKey` and `Resend:FromEmail`.

## Problem
The booking pass page tells the user "Your tour pass has been sent to [email]" but no email is ever sent. Without this, the user has no offline record of their reference code and no way to retrieve their pass later (TP-001) unless they screenshot the page immediately.

## Expected behaviour
On booking confirmation, a transactional email is sent containing the reference code and a direct link to the pass. This is the user's primary proof of booking to show at reception.

## Email content (minimum)
- Subject: `Your TourPortal booking is confirmed — Ref: #TP-XXXX-X`
- Tour title and travel dates
- Lead traveler name and guest count
- Total price (to pay at reception)
- Booking reference code (prominent)
- Direct link to `/booking/lookup?ref={referenceCode}` to retrieve the pass at any time
- Reminder to bring this reference to the physical office

## Acceptance criteria
- [ ] Resend client configured in `Program.cs` using `appsettings` values
- [ ] Email is sent inside `BookingStateService.CreateBooking()` after Supabase insert succeeds
- [ ] Send failure is handled gracefully — booking is still created, user is shown a warning that email may not have arrived
- [ ] UI copy ("sent to your inbox") is only displayed after a confirmed send attempt
