# TP-004 · QR code is a static SVG placeholder — cannot be scanned

**Priority:** Medium  
**Type:** Bug / Missing Feature

## Problem
The QR code displayed on the pass is a hardcoded decorative SVG labelled "QR placeholder · Preview only". It encodes no real data and cannot be scanned by a standard QR reader.

## Expected behaviour
The QR code should encode the pass lookup URL so the receptionist (or anyone) can scan it to pull up the booking instantly, without typing the reference code manually.

## What to encode
```
https://{domain}/booking/lookup?ref={referenceCode}
```

This URL opens the lookup page pre-filled, so scanning the QR code at reception immediately shows the booking.

## Acceptance criteria
- [ ] QR code is generated dynamically using the booking's `reference_code`
- [ ] Encoded value is `/booking/lookup?ref={referenceCode}`
- [ ] QR code is scannable by standard phone cameras and QR readers
- [ ] "QR placeholder · Preview only" label is removed
- [ ] Works on the printed guide sheet as well as the on-screen pass

## Dependencies
Requires TP-001 (`/booking/lookup` page) to exist for the encoded URL to resolve.

## Implementation note
Consider a JavaScript QR library (e.g. `qrcode.js` via CDN) injected via `IJSRuntime`, or a server-side .NET QR library (e.g. `QRCoder`) to generate the code as a base64 image.
