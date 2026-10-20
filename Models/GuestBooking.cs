namespace TourPortal.Models;

public class GuestBooking
{
    public string ReferenceCode { get; set; } = string.Empty;
    public string LeadTravelerName { get; set; } = "Alex Morgan";
    public string Email { get; set; } = "alex.morgan@example.com";
    public string Phone { get; set; } = "+1 (415) 555-0138";
    public int GuestCount { get; set; } = 2;
    public string TravelDates { get; set; } = "12–14 Jun 2027";
    public decimal TotalPaid { get; set; } = 496m;
    public string TourId { get; set; } = "tour-ubud-naturally";
    public string TourTitle { get; set; } = "Ubud, naturally";
    public string TourImageUrl { get; set; } = "https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=1200&q=80";
    public string TourLocation { get; set; } = "Ubud, Bali, Indonesia";
    public decimal PricePerPerson { get; set; } = 248m;
}
