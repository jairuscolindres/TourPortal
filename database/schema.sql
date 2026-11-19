-- ==============================================================================
-- TourPortal - Supabase Database Schema
-- Based on: CIT-U Application Development Lecture (Part VI: Project Milestone)
-- ==============================================================================

-- 1. BOOKINGS TABLE
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
  deleted_at timestamptz, -- Habit 4 / Rubric 5: Soft delete for cancellation/archive tracking
  created_at timestamptz not null default now()
);

-- Fast Habit 1: Add indexes on columns searched or filtered on
create index on public.bookings (reference_code);
create index on public.bookings (email);
create index on public.bookings (tour_id);

-- Lock 1: Grant permissions to anon role
grant select, insert, update, delete on public.bookings to anon;

-- Lock 2: Enable Row Level Security (RLS)
alter table public.bookings enable row level security;

-- Open policies for class milestone
create policy "read_all" on public.bookings for select to anon using (true);
create policy "insert_all" on public.bookings for insert to anon with check (true);
create policy "update_all" on public.bookings for update to anon using (true) with check (true);
create policy "delete_all" on public.bookings for delete to anon using (true);


-- 2. TOURS TABLE (For TP-009 Catalog Migration)
create table public.tours (
  id bigint generated always as identity primary key,
  slug text not null unique,
  title text not null,
  location text not null,
  country text not null,
  category text not null,
  price numeric(10,2) not null default 0 check (price >= 0),
  rating numeric(3,2) not null default 5.0,
  review_count integer not null default 0,
  duration text not null,
  image_url text not null,
  description text not null,
  group_size integer not null default 8 check (group_size >= 1),
  office_id text,
  deleted_at timestamptz, -- Soft delete: deactivating a tour without breaking booking history
  created_at timestamptz not null default now()
);

create index on public.tours (slug);
create index on public.tours (category);

grant select, insert, update, delete on public.tours to anon;
alter table public.tours enable row level security;

create policy "read_all" on public.tours for select to anon using (true);
create policy "insert_all" on public.tours for insert to anon with check (true);
create policy "update_all" on public.tours for update to anon using (true) with check (true);
create policy "delete_all" on public.tours for delete to anon using (true);
