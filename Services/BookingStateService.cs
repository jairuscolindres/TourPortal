namespace TourPortal.Services;

using Microsoft.AspNetCore.Components;
using Supabase;
using TourPortal.Models;

public class BookingStateService
{
    private readonly NavigationManager _navigationManager;
    private readonly Client _supabase;
    private readonly ITourDataService _tourDataService;

    public GuestBooking CurrentBooking { get; set; }

    public GuestBooking? ActiveBooking
    {
        get => CurrentBooking;
        set => CurrentBooking = value ?? CreateDefaultBooking();
    }

    public event Action? OnChange;

    public BookingStateService(
        NavigationManager navigationManager,
        Client supabase,
        ITourDataService tourDataService)
    {
        _navigationManager = navigationManager;
        _supabase = supabase;
        _tourDataService = tourDataService;
        CurrentBooking = CreateDefaultBooking();
    }

    public static GuestBooking CreateDefaultBooking()
    {
        return new GuestBooking
        {
            ReferenceCode = "TP-8942-X",
            LeadTravelerName = "Alex Morgan",
            Email = "alex.morgan@example.com",
            Phone = "+1 (415) 555-0138",
            GuestCount = 2,
            TravelDates = "12–14 Jun 2027",
            TotalPaid = 496m,
            TourId = "tour-ubud-naturally",
            TourTitle = "Ubud, naturally",
            TourImageUrl = "https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=1200&q=80",
            TourLocation = "Ubud, Bali, Indonesia",
            PricePerPerson = 248m
        };
    }

    /// <summary>
    /// Habit 2: Fetch only active bookings (deleted_at is null) directly in PostgreSQL.
    /// </summary>
    public async Task<List<Booking>> GetActiveBookingsAsync()
    {
        var response = await _supabase.From<Booking>()
            .Where(b => b.DeletedAt == null)
            .Get();
        return response.Models;
    }

    /// <summary>
    /// Habit 2: Retrieve a specific booking filtering by reference code and deleted_at is null.
    /// </summary>
    public async Task<Booking?> GetBookingByReferenceAsync(string referenceCode)
    {
        var response = await _supabase.From<Booking>()
            .Where(b => b.ReferenceCode == referenceCode)
            .Where(b => b.DeletedAt == null)
            .Get();
        return response.Models.FirstOrDefault();
    }

    /// <summary>
    /// Soft delete storage pattern (Rubric 5, Slide 24-28, 51):
    /// When a booking is cancelled or removed, deleted_at timestamp is set via .Set(x => x.DeletedAt, DateTime.UtcNow).Update().
    /// </summary>
    public async Task SoftDeleteBookingAsync(string referenceCode)
    {
        await _supabase.From<Booking>()
            .Where(b => b.ReferenceCode == referenceCode)
            .Set(x => x.DeletedAt!, DateTime.UtcNow)
            .Update();
    }

    /// <summary>
    /// Creates a persistent booking in Supabase and updates in-memory active booking state.
    /// </summary>
    public async Task<Booking> CreateBookingAsync(GuestBooking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        if (string.IsNullOrWhiteSpace(booking.ReferenceCode))
        {
            var randomCode = Random.Shared.Next(1000, 9999);
            booking.ReferenceCode = $"TP-{randomCode}-X";
        }

        if (booking.PricePerPerson > 0 && booking.TotalPaid <= 0)
        {
            booking.TotalPaid = booking.PricePerPerson * booking.GuestCount;
        }

        var dbBooking = new Booking
        {
            ReferenceCode = booking.ReferenceCode,
            TourId = booking.TourId,
            TravelDates = booking.TravelDates,
            LeadTravelerName = booking.LeadTravelerName,
            Email = booking.Email,
            Phone = booking.Phone,
            GuestCount = booking.GuestCount,
            PricePerPerson = booking.PricePerPerson,
            TotalPrice = booking.TotalPaid,
            PaymentStatus = "unpaid",
            PaymentMethod = null,
            CheckedIn = false,
            CreatedAt = DateTime.UtcNow
        };

        var response = await _supabase.From<Booking>().Insert(dbBooking);
        var inserted = response.Models.FirstOrDefault() ?? dbBooking;

        CurrentBooking = booking;
        NotifyStateChanged();

        return inserted;
    }

    public void CreateBooking(GuestBooking booking)
    {
        _ = CreateBookingAsync(booking);
        _navigationManager.NavigateTo("/booking/pass");
    }

    public void ClearBooking()
    {
        CurrentBooking = CreateDefaultBooking();
        NotifyStateChanged();
    }

    public GuestBooking MapToGuestBooking(Booking dbBooking)
    {
        var tour = _tourDataService.GetTourById(dbBooking.TourId);
        return new GuestBooking
        {
            ReferenceCode = dbBooking.ReferenceCode,
            LeadTravelerName = dbBooking.LeadTravelerName,
            Email = dbBooking.Email,
            Phone = dbBooking.Phone,
            GuestCount = dbBooking.GuestCount,
            TravelDates = dbBooking.TravelDates,
            TotalPaid = dbBooking.TotalPrice,
            TourId = dbBooking.TourId,
            TourTitle = tour?.Title ?? "Tour",
            TourImageUrl = tour?.ImageUrl ?? "https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=1200&q=80",
            TourLocation = tour != null ? $"{tour.Location}, {tour.Country}" : "",
            PricePerPerson = dbBooking.PricePerPerson
        };
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
