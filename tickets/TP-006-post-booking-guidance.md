# TP-006 · Dead-end UX after pass generation — no "what next" guidance

**Priority:** Low  
**Status:** Partially addressed — physical guide sheet covers this at reception

## Decision
The receptionist hands the traveler a **physical printed guide sheet** when they arrive at the office. This sheet contains the meeting point, itinerary, what to bring, and guide contact details — removing the need to surface all of this digitally on the pass page.

The pass page itself only needs to tell the user **to bring their reference code to the office**. That is now covered by the confirmation email (TP-003).

## Remaining gap
The pass page still has no visible instruction telling the user what to do next (i.e. go to the office). This is a copy/UX fix only.

## Acceptance criteria
- [ ] Pass page includes a clear "Next step" callout: bring your reference code to our office at [address] before your departure date to collect your printed guide and complete payment
- [ ] Office address and opening hours are visible on the pass page
- [ ] "Back to explore" remains as the only navigation exit (no other changes needed)

## Out of scope
- Digital "what to bring" section (covered by physical guide sheet)
- Tour-specific meeting point on the pass page (covered by physical guide sheet)
- Guide contact details on the pass page (covered by physical guide sheet)
