# TP-002 · No persistent booking storage — bookings vanish on session end

**Status:** ✅ Completed  
**Priority:** High  
**Type:** Infrastructure / Milestone Feature  
**Reference:** CIT-U Application Development Lecture — *APIs and Databases: Where Your App's Data Lives (Project Milestone)*

## Decision
Use **Supabase** (Route C: Ready-made backend) via NuGet package `Supabase`.  
Config keys live in `appsettings.json` and `appsettings.Development.json` under `Supabase:Url` and `Supabase:Key`.

## Problem
`BookingStateService` is currently scoped in-memory. If the user refreshes or closes the page, the data is gone ("The problem: your app forgets", Slide 3). Data must be kept permanently in Supabase PostgreSQL tables.

## Database Schema (`database/schema.sql`)
The exact SQL script must be stored at `database/schema.sql` (Slide 45 & 52):

```sql
create table public.bookings (
  id bigint generated always as identity primary key,
  reference_code text not null unique,
  tour_id text not null,
  travel_dates text not null,
  lead_traveler_name text not null,
  email text not null,
  phone text not null,
  guest_count integer not null default 1 check (guest_count >= 1),
  price_per_person numeric(10,2) not null default 0 check (price_per_person >= 0),
  total_price numeric(10,2) not null default 0 check (total_price >= 0),
  payment_status text not null default 'unpaid' check (payment_status in ('unpaid', 'paid', 'cancelled')),
  payment_method text,
  checked_in boolean not null default false,
  deleted_at timestamptz, -- Soft delete storage pattern (Rubric 5 / Slide 25)
  created_at timestamptz not null default now()
);

-- Habit 1: Index on columns searched or filtered on (Slide 18)
create index on public.bookings (reference_code);
create index on public.bookings (email);
create index on public.bookings (tour_id);

-- Lock 1: GRANT permissions to anon (Slide 39, 45)
grant select, insert, update, delete on public.bookings to anon;

-- Lock 2: Enable Row Level Security (RLS) (Slide 39, 45)
alter table public.bookings enable row level security;

-- Open policies for class milestone (Slide 45)
create policy "read_all" on public.bookings for select to anon using (true);
create policy "insert_all" on public.bookings for insert to anon with check (true);
create policy "update_all" on public.bookings for update to anon using (true) with check (true);
create policy "delete_all" on public.bookings for delete to anon using (true);
```

## C# Model Standards (Slide 48)
Model inherits from `Supabase.Postgrest.Models.BaseModel`:
- `[Table("bookings")]`
- `[PrimaryKey("id", false)]` — `false` indicates DB generates identity ID on insert
- `[Column("column_name")]` on every mapped property
- `[Column("deleted_at")] public DateTime? DeletedAt { get; set; }`

## DI Registration in `Program.cs` (Slide 47)
```csharp
var url = builder.Configuration["Supabase:Url"]!;
var key = builder.Configuration["Supabase:Key"]!;
var supabase = new Supabase.Client(url, key, new Supabase.SupabaseOptions { AutoConnectRealtime = false });
await supabase.InitializeAsync();
builder.Services.AddSingleton(supabase);
```

## Fast Habits (Slide 17–20, 49)
1. **Habit 1**: Database indexes on `reference_code`, `email`, and `tour_id`.
2. **Habit 2**: Fetch only what is needed — put `.Where(b => b.DeletedAt == null)` before `.Get()` so PostgreSQL filters before transmitting.
3. **Habit 3**: Single request — never query inside loops.
4. **Prevent Double POST (Slide 8, 50)**: Disable booking submit button while request is saving.

## Soft Delete Storage Pattern (Rubric 5, Slide 24–28, 51)
- When a booking is cancelled or removed, `deleted_at` timestamp is set via `.Set(x => x.DeletedAt, DateTime.UtcNow).Update()`.
- Rows are not dropped from PostgreSQL, preserving audit and historical data for business reporting.

## Acceptance Criteria
- [x] `database/schema.sql` created in repository and executed in Supabase SQL editor
- [x] `Supabase` package installed in `TourPortal.csproj`
- [x] `Supabase:Url` and `Supabase:Key` read from configuration and registered in `Program.cs`
- [x] C# `Booking` model maps to `public.bookings` using `[Table]`, `[PrimaryKey]`, and `[Column]`
- [x] Creating a booking disables the button while saving to prevent double POSTs
- [x] Booking retrieval filters `.Where(b => b.DeletedAt == null)` in database
- [x] Soft delete is supported via `deleted_at` timestamp updates
