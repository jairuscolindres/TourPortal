namespace TourPortal.Models;

using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("bookings")]
public class Booking : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }

    [Column("reference_code")]
    public string ReferenceCode { get; set; } = string.Empty;

    [Column("tour_id")]
    public string TourId { get; set; } = string.Empty;

    [Column("travel_dates")]
    public string TravelDates { get; set; } = string.Empty;

    [Column("lead_traveler_name")]
    public string LeadTravelerName { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("phone")]
    public string Phone { get; set; } = string.Empty;

    [Column("guest_count")]
    public int GuestCount { get; set; } = 1;

    [Column("price_per_person")]
    public decimal PricePerPerson { get; set; }

    [Column("total_price")]
    public decimal TotalPrice { get; set; }

    [Column("payment_status")]
    public string PaymentStatus { get; set; } = "unpaid";

    [Column("payment_method")]
    public string? PaymentMethod { get; set; }

    [Column("checked_in")]
    public bool CheckedIn { get; set; } = false;

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
