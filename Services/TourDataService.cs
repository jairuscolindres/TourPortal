namespace TourPortal.Services;

using TourPortal.Models;

public class TourDataService : ITourDataService
{
    private readonly List<Tour> _tours;

    public TourDataService()
    {
        _tours = new List<Tour>
        {
            new Tour
            {
                Id = "tour-ubud-naturally",
                Title = "Ubud, naturally",
                Location = "Ubud, Bali",
                Country = "Bali, Indonesia",
                Category = "Nature",
                Price = 248m,
                Rating = 4.98,
                ReviewCount = 128,
                Duration = "3 days · 2 nights",
                ImageUrl = "https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=1200&q=80",
                CoverCaption = "Tegallalang rice terraces, Ubud",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1518548419970-58e3b4079ab2?auto=format&fit=crop&w=800&q=80",
                    "https://images.unsplash.com/photo-1555400038-63f5ba517a47?auto=format&fit=crop&w=800&q=80"
                },
                Description = "Take the scenic route through Bali's green heart. Walk the rice terraces with a local guide, find a waterfall off the beaten path, and share a meal made with the island's freshest ingredients. Three unhurried days, a small group, and a deeper connection to Ubud.",
                SubCategory = "NATURE · SLOW TRAVEL",
                Collection = "THE BALI COLLECTION",
                GroupSize = "Up to 8 travelers",
                GuideInfo = "English-speaking guide",
                LeaderInfo = "Locally led",
                AvailableDates = "12–14 Jun 2027",
                Inclusions = new List<string>
                {
                    "Expert local guide for all 3 days",
                    "All activity entrance fees",
                    "2 nights in a boutique eco-lodge",
                    "Air-conditioned local transfers",
                    "Breakfasts & Balinese welcome lunch",
                    "Refillable water throughout your trip"
                },
                ItineraryDays = new List<TourItineraryDay>
                {
                    new TourItineraryDay
                    {
                        DayNumber = 1,
                        DateLabel = "12 Jun",
                        FullDateLabel = "Saturday, 12 June 2027 · Ubud, Bali",
                        Title = "Rice terraces & local flavors",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "08:30 – 09:00",
                                Title = "Meet your local guide",
                                Description = "A warm welcome and a look at the days ahead. Meet at the Ubud Palace main entrance.",
                                Duration = "30 min",
                                Guests = "2 guests",
                                IconType = "pin"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "09:00 – 11:00",
                                Title = "Tegallalang rice terrace walk",
                                Description = "Follow emerald-green trails and discover the story behind Bali's ancient subak irrigation.",
                                Duration = "2 hours",
                                Guests = "2 guests",
                                IconType = "mountain"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "12:00 – 13:00",
                                Title = "Farm-to-table Balinese lunch",
                                Description = "Seasonal island flavors, a shared table, and a beautiful view. Lunch is included.",
                                Duration = "1 hour",
                                Guests = "2 guests",
                                IconType = "food"
                            }
                        }
                    },
                    new TourItineraryDay
                    {
                        DayNumber = 2,
                        DateLabel = "13 Jun",
                        FullDateLabel = "Sunday, 13 June 2027 · Ubud, Bali",
                        Title = "Waterfalls & village life",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "09:00 – 11:30",
                                Title = "Hidden ravine & jungle waterfall",
                                Description = "Trek through bamboo groves to a secluded canyon waterfall off the standard tourist trail.",
                                Duration = "2.5 hours",
                                Guests = "2 guests",
                                IconType = "water"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "13:00 – 15:00",
                                Title = "Traditional woodcarving workshop",
                                Description = "Learn generational woodworking craft from a family guild in Mas village.",
                                Duration = "2 hours",
                                Guests = "2 guests",
                                IconType = "temple"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "17:00 – 18:30",
                                Title = "Sunset herbal tea & meditation",
                                Description = "Slow down with native Balinese jamu infusion overlooking the Ayung river valley.",
                                Duration = "1.5 hours",
                                Guests = "2 guests",
                                IconType = "food"
                            }
                        }
                    },
                    new TourItineraryDay
                    {
                        DayNumber = 3,
                        DateLabel = "14 Jun",
                        FullDateLabel = "Monday, 14 June 2027 · Ubud, Bali",
                        Title = "Sacred springs & a slow goodbye",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "08:30 – 10:30",
                                Title = "Tirta Empul water blessing",
                                Description = "Experience a quiet morning purification ceremony accompanied by a local temple guardian.",
                                Duration = "2 hours",
                                Guests = "2 guests",
                                IconType = "temple"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "11:30 – 13:00",
                                Title = "Farewell artisan brunch",
                                Description = "A relaxed final gathering at an open-air organic pavilion before departure.",
                                Duration = "1.5 hours",
                                Guests = "2 guests",
                                IconType = "food"
                            }
                        }
                    }
                }
            },
            new Tour
            {
                Id = "tour-penida-hidden-coast",
                Title = "The hidden coast of Penida",
                Location = "Nusa Penida, Bali",
                Country = "Bali, Indonesia",
                Category = "Beach",
                Price = 88m,
                Rating = 4.94,
                ReviewCount = 94,
                Duration = "1 day · Local guide · Small group",
                ImageUrl = "https://images.unsplash.com/photo-1544644181-1484b3fdfc62?auto=format&fit=crop&w=1200&q=80",
                CoverCaption = "Kelingking secluded cliff coast, Nusa Penida",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1544644181-1484b3fdfc62?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?auto=format&fit=crop&w=800&q=80",
                    "https://images.unsplash.com/photo-1512100356356-de1b84283e18?auto=format&fit=crop&w=800&q=80"
                },
                Description = "Venture beyond the crowded cliffs into secluded coves, turquoise lagoons, and pristine manta ray sanctuaries along Nusa Penida's dramatic coastline.",
                SubCategory = "COASTAL ESCAPE · ADVENTURE",
                Collection = "THE BALI COLLECTION",
                GroupSize = "Up to 6 travelers",
                GuideInfo = "English-speaking guide",
                LeaderInfo = "Locally led",
                AvailableDates = "16 Jun 2027",
                Inclusions = new List<string>
                {
                    "Roundtrip high-speed boat transfers from Sanur",
                    "Snorkeling gear & marine conservation pass",
                    "Private air-conditioned 4x4 island transport",
                    "Fresh seafood beachside lunch & tropical fruit bar",
                    "Underwater photography coverage included",
                    "Certified local marine safety guide"
                },
                ItineraryDays = new List<TourItineraryDay>
                {
                    new TourItineraryDay
                    {
                        DayNumber = 1,
                        DateLabel = "16 Jun",
                        FullDateLabel = "Wednesday, 16 June 2027 · Nusa Penida, Bali",
                        Title = "Untamed cliffs & crystal lagoons",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "07:30 – 08:30",
                                Title = "Sanur harbor speedboat crossing",
                                Description = "Morning boat transfer across the Badung Strait to Nusa Penida harbour.",
                                Duration = "1 hour",
                                Guests = "2 guests",
                                IconType = "water"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "09:30 – 12:00",
                                Title = "Manta Bay guided snorkel",
                                Description = "Swim alongside gentle giant manta rays in clear protected coastal bays.",
                                Duration = "2.5 hours",
                                Guests = "2 guests",
                                IconType = "water"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "13:00 – 14:30",
                                Title = "Seafood lunch at Crystal Bay",
                                Description = "Grilled mahi-mahi with sambal matah under shady coconut palms.",
                                Duration = "1.5 hours",
                                Guests = "2 guests",
                                IconType = "food"
                            }
                        }
                    }
                }
            },
            new Tour
            {
                Id = "tour-kyoto-beyond-guidebook",
                Title = "Kyoto beyond the guidebook",
                Location = "Kyoto, Japan",
                Country = "Japan",
                Category = "Culture",
                Price = 126m,
                Rating = 4.96,
                ReviewCount = 112,
                Duration = "1 day · Local guide · Small group",
                ImageUrl = "https://images.unsplash.com/photo-1493976040374-85c8e12f0c0e?auto=format&fit=crop&w=1200&q=80",
                CoverCaption = "Historic Higashiyama twilight, Kyoto",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1493976040374-85c8e12f0c0e?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1503899036084-c55cdd92da26?auto=format&fit=crop&w=800&q=80",
                    "https://images.unsplash.com/photo-1528360983277-13d401cdc186?auto=format&fit=crop&w=800&q=80"
                },
                Description = "Walk the historic cobblestones of Gion and Arashiyama with a local cultural historian. Gain private access to secluded zen gardens, artisan tea ceremonies, and pottery workshops.",
                SubCategory = "HERITAGE · MINDFUL EXPLORATION",
                Collection = "THE JAPAN ESSENTIALS",
                GroupSize = "Up to 6 travelers",
                GuideInfo = "English-speaking licensed historian",
                LeaderInfo = "Locally led",
                AvailableDates = "20 Jun 2027",
                Inclusions = new List<string>
                {
                    "Licensed English-speaking Kyoto heritage historian",
                    "Private access pass to secluded Zen monastery gardens",
                    "Traditional Machiya heritage tea ceremony",
                    "Seasonal multi-course Kaiseki luncheon",
                    "Kyoto private electric bicycle hire and transit card",
                    "Artisanal matcha preparation kit gift"
                },
                ItineraryDays = new List<TourItineraryDay>
                {
                    new TourItineraryDay
                    {
                        DayNumber = 1,
                        DateLabel = "20 Jun",
                        FullDateLabel = "Sunday, 20 June 2027 · Kyoto, Japan",
                        Title = "Zen courtyards & historic alleys",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "08:30 – 10:30",
                                Title = "Early morning Daitoku-ji private visit",
                                Description = "Walk through quiet stone gardens before public opening hours.",
                                Duration = "2 hours",
                                Guests = "2 guests",
                                IconType = "temple"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "11:30 – 13:00",
                                Title = "Artisan Machiya tea ceremony",
                                Description = "Experience traditional Urasenke tea preparation with an elder master.",
                                Duration = "1.5 hours",
                                Guests = "2 guests",
                                IconType = "food"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "14:30 – 16:30",
                                Title = "Hidden Gion preservation walk",
                                Description = "Explore bamboo pathways and traditional wooden machiya architecture.",
                                Duration = "2 hours",
                                Guests = "2 guests",
                                IconType = "pin"
                            }
                        }
                    }
                }
            },
            new Tour
            {
                Id = "tour-alps-quieter-side",
                Title = "A quieter side of the Alps",
                Location = "Dolomites, Italy",
                Country = "Italy",
                Category = "Eco",
                Price = 320m,
                Rating = 4.97,
                ReviewCount = 89,
                Duration = "2 days · Local guide · Small group",
                ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=1200&q=80",
                CoverCaption = "Lago di Braies & high alpine peaks, South Tyrol",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1519681393784-d120267933ba?auto=format&fit=crop&w=800&q=80",
                    "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?auto=format&fit=crop&w=800&q=80"
                },
                Description = "Traverse dramatic limestone pinnacles and serene alpine meadows along quiet, lesser-known trails while staying in family-owned rifugios powered entirely by renewable energy.",
                SubCategory = "ALPINE TREK · SUSTAINABILITY",
                Collection = "THE DOLOMITES ARCHIVE",
                GroupSize = "Up to 8 travelers",
                GuideInfo = "UIAGM certified mountain guide",
                LeaderInfo = "Locally led",
                AvailableDates = "24–25 Jun 2027",
                Inclusions = new List<string>
                {
                    "Boutique eco-rifugio lodging with mountain panorama",
                    "Certified UIAGM Alpine mountain guide throughout",
                    "Artisanal South Tyrol alpine cheese and wine tastings",
                    "All high-altitude cable car and lift passes",
                    "Luggage transfer service between mountain refuges",
                    "Nordic trekking poles & safety equipment"
                },
                ItineraryDays = new List<TourItineraryDay>
                {
                    new TourItineraryDay
                    {
                        DayNumber = 1,
                        DateLabel = "24 Jun",
                        FullDateLabel = "Thursday, 24 June 2027 · Dolomites, Italy",
                        Title = "Lakeside trails & high meadows",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "08:00 – 10:30",
                                Title = "Alpine lake sunrise traverse",
                                Description = "Morning walk along emerald shores up toward the high limestone pastures.",
                                Duration = "2.5 hours",
                                Guests = "2 guests",
                                IconType = "mountain"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "12:30 – 14:00",
                                Title = "Rifugio farmstead lunch",
                                Description = "Handmade canederli and mountain herbal tea prepared with regional ingredients.",
                                Duration = "1.5 hours",
                                Guests = "2 guests",
                                IconType = "food"
                            }
                        }
                    },
                    new TourItineraryDay
                    {
                        DayNumber = 2,
                        DateLabel = "25 Jun",
                        FullDateLabel = "Friday, 25 June 2027 · Dolomites, Italy",
                        Title = "Pinnacle ridge & valley descent",
                        Items = new List<TourItineraryItem>
                        {
                            new TourItineraryItem
                            {
                                TimeRange = "08:30 – 12:00",
                                Title = "High col ridge crossing",
                                Description = "Stunning 360-degree vistas across the Tre Cime and Sella mountain massifs.",
                                Duration = "3.5 hours",
                                Guests = "2 guests",
                                IconType = "mountain"
                            },
                            new TourItineraryItem
                            {
                                TimeRange = "13:00 – 14:30",
                                Title = "Artisanal cheese cellar tasting",
                                Description = "Sample aged mountain cheeses and regional speck before valley departure.",
                                Duration = "1.5 hours",
                                Guests = "2 guests",
                                IconType = "food"
                            }
                        }
                    }
                }
            }
        };
    }

    public IReadOnlyList<Tour> GetAllTours() => _tours.AsReadOnly();

    public Tour? GetTourById(string id) =>
        _tours.FirstOrDefault(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase));

    public Task<IReadOnlyList<Tour>> GetAllToursAsync() =>
        Task.FromResult<IReadOnlyList<Tour>>(_tours.AsReadOnly());

    public Task<Tour?> GetTourByIdAsync(string id) =>
        Task.FromResult(_tours.FirstOrDefault(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase)));
}
