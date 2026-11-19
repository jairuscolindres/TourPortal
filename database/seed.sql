-- ==============================================================================
-- TourPortal - Seed Data Script
-- Reference: TP-002.5 / CIT-U Application Development Lecture (Slide 46)
-- Run this in Supabase SQL Editor after running schema.sql
-- ==============================================================================

-- 1. SEED TOURS
insert into public.tours (slug, title, location, country, category, price, rating, review_count, duration, image_url, description, group_size, office_id)
values
(
  'tour-ubud-naturally',
  'Ubud, naturally',
  'Ubud, Bali',
  'Bali, Indonesia',
  'Nature',
  248.00,
  4.98,
  128,
  '3 days · 2 nights',
  'https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=1200&q=80',
  'Take the scenic route through Bali''s green heart. Walk the rice terraces with a local guide, find a waterfall off the beaten path, and share a meal made with the island''s freshest ingredients.',
  8,
  'bali'
),
(
  'tour-penida-hidden-coast',
  'The hidden coast of Penida',
  'Nusa Penida, Bali',
  'Bali, Indonesia',
  'Beach',
  88.00,
  4.94,
  94,
  '1 day · Local guide · Small group',
  'https://images.unsplash.com/photo-1544644181-1484b3fdfc62?auto=format&fit=crop&w=1200&q=80',
  'Venture beyond the crowded cliffs into secluded coves, turquoise lagoons, and pristine manta ray sanctuaries along Nusa Penida''s dramatic coastline.',
  6,
  'bali'
),
(
  'tour-kyoto-beyond-guidebook',
  'Kyoto beyond the guidebook',
  'Kyoto, Japan',
  'Japan',
  'Culture',
  126.00,
  4.96,
  112,
  '1 day · Local guide · Small group',
  'https://images.unsplash.com/photo-1493976040374-85c8e12f0c0e?auto=format&fit=crop&w=1200&q=80',
  'Walk the historic cobblestones of Gion and Arashiyama with a local cultural historian. Gain private access to secluded zen gardens, artisan tea ceremonies, and pottery workshops.',
  6,
  'kyoto'
),
(
  'tour-alps-quieter-side',
  'A quieter side of the Alps',
  'Dolomites, Italy',
  'Italy',
  'Eco',
  320.00,
  4.97,
  89,
  '2 days · Local guide · Small group',
  'https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=1200&q=80',
  'Traverse dramatic limestone pinnacles and serene alpine meadows along quiet, lesser-known trails while staying in family-owned rifugios powered entirely by renewable energy.',
  8,
  'dolomites'
);

-- 2. SEED INITIAL BOOKINGS
insert into public.bookings (reference_code, tour_id, travel_dates, lead_traveler_name, email, phone, guest_count, price_per_person, total_price, payment_status, payment_method, checked_in)
values
(
  'TP-8942-X',
  'tour-ubud-naturally',
  '12–14 Jun 2027',
  'Alex Morgan',
  'alex.morgan@example.com',
  '+1 (415) 555-0138',
  2,
  248.00,
  496.00,
  'unpaid',
  null,
  false
),
(
  'TP-5120-B',
  'tour-alps-quieter-side',
  '24–25 Jun 2027',
  'Elena Rossi',
  'elena.rossi@example.com',
  '+39 02 555 0192',
  1,
  320.00,
  320.00,
  'paid',
  'card',
  true
),
(
  'TP-3301-K',
  'tour-kyoto-beyond-guidebook',
  '20 Jun 2027',
  'Kenji Sato',
  'kenji.sato@example.com',
  '+81 75 555 0144',
  4,
  126.00,
  504.00,
  'paid',
  'cash',
  false
);
