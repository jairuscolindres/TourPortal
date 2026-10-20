namespace TourPortal.Models;

public class GuestTraveler
{
    public string LeadTravelerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int GuestCount { get; set; } = 1;
}
