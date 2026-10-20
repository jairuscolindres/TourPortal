namespace TourPortal.Models;

public class TourItineraryItem
{
    public string TimeRange { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Guests { get; set; } = "2 guests";
    public string IconType { get; set; } = "pin"; // "pin", "mountain", "food", "temple", "water"
}

public class TourItineraryDay
{
    public int DayNumber { get; set; }
    public string DateLabel { get; set; } = string.Empty; // e.g. "12 Jun"
    public string FullDateLabel { get; set; } = string.Empty; // e.g. "Saturday, 12 June 2027 · Ubud, Bali"
    public string Title { get; set; } = string.Empty; // e.g. "Rice terraces & local flavors"
    public List<TourItineraryItem> Items { get; set; } = new();
}

public class Tour
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // "Nature", "Beach", "Culture", "Eco"
    public decimal Price { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; } = 128;
    public string Duration { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string CoverCaption { get; set; } = string.Empty;
    public List<string> GalleryImages { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string SubCategory { get; set; } = "NATURE · SLOW TRAVEL";
    public string Collection { get; set; } = "THE BALI COLLECTION";
    public string GroupSize { get; set; } = "Up to 8 travelers";
    public string GuideInfo { get; set; } = "English-speaking guide";
    public string LeaderInfo { get; set; } = "Locally led";
    public string AvailableDates { get; set; } = "12–14 Jun 2027";
    public List<string> Inclusions { get; set; } = new();
    public List<TourItineraryDay> ItineraryDays { get; set; } = new();
}
