# TP-008 · Office/meetup location is missing — no regional office seeded per tour

**Priority:** Medium  
**Type:** Data Model / Feature

## Context
Each tour takes place in a different country. The pass page and physical guide sheet need to tell the user which physical office to visit before their departure date to collect their guide sheet and pay. A single global address makes no sense across Bali, Kyoto, and Italy.

## Decision
Add an `Office` object to the `Tour` model and seed a fake regional office per tour collection. Office is scoped to the tour, not globally shared.

## Model change — `Tour.cs`

Add a nested `TourOffice` class and an `Office` property to `Tour`:

```csharp
public class TourOffice
{
    public string Name { get; set; } = string.Empty;        // e.g. "TourPortal Bali"
    public string Address { get; set; } = string.Empty;     // e.g. "Jl. Raya Ubud No. 7, Ubud"
    public string City { get; set; } = string.Empty;        // e.g. "Ubud, Bali 80571"
    public string OpeningHours { get; set; } = string.Empty; // e.g. "Mon–Sat · 08:00–17:00 WITA"
    public string Phone { get; set; } = string.Empty;       // e.g. "+62 361 971 234"
}
```

## Seeded offices (fake data)

| Tour(s) | Office Name | Address | Hours |
|---|---|---|---|
| Bali collection | TourPortal Bali | Jl. Raya Ubud No. 7, Ubud, 80571 | Mon–Sat · 08:00–17:00 WITA |
| Japan collection | TourPortal Kyoto | 15 Nishiki-machi, Higashiyama-ku, Kyoto 605-0073 | Tue–Sun · 09:00–18:00 JST |
| Dolomites collection | TourPortal Dolomites | Via Dolomiti 3, Cortina d'Ampezzo, 32043 | Mon–Fri · 08:30–17:30 CET |

## Where office data surfaces

1. **Pass page** (`BookingPass.razor`) — "Next step" callout:  
   *"Bring your reference code to [Office Name] at [Address] before [travel date] to collect your guide sheet and complete payment."*

2. **Physical guide sheet** (print layout) — office name and address in the header or footer

3. **Receptionist dashboard** (`/reception`) — dashboard header shows which office the receptionist is operating from (derived from the tours they manage)

## Acceptance criteria
- [ ] `TourOffice` class added to `Tour.cs`
- [ ] `Office` property added to `Tour` class
- [ ] All 4 tours in `TourDataService` seeded with a fake regional office
- [ ] Pass page "Next step" callout displays `Tour.Office.Name`, `Tour.Office.Address`, and `Tour.Office.OpeningHours`
- [ ] Physical guide sheet print layout includes office name and address
- [ ] Receptionist dashboard header reflects the office associated with today's tours

## Dependencies
- Requires TP-006 (pass page "next step" copy) to be in scope for the callout
- Requires TP-007 (receptionist dashboard) for the dashboard header
