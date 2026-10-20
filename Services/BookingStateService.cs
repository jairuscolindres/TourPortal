namespace TourPortal.Services;

using Microsoft.AspNetCore.Components;
using TourPortal.Models;

public class BookingStateService
{
    private readonly NavigationManager _navigationManager;

    public GuestBooking CurrentBooking { get; set; }

    public GuestBooking? ActiveBooking
    {
        get => CurrentBooking;
        set => CurrentBooking = value ?? CreateDefaultBooking();
    }

    public event Action? OnChange;

    public BookingStateService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        CurrentBooking = CreateDefaultBooking();
    }

    private static GuestBooking CreateDefaultBooking()
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

    public void CreateBooking(GuestBooking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        var randomCode = Random.Shared.Next(1000, 9999);
        booking.ReferenceCode = $"TP-{randomCode}-X";

        if (booking.PricePerPerson > 0)
        {
            booking.TotalPaid = booking.PricePerPerson * booking.GuestCount;
        }

        CurrentBooking = booking;
        NotifyStateChanged();

        _navigationManager.NavigateTo("/booking/pass");
    }

    public void ClearBooking()
    {
        CurrentBooking = CreateDefaultBooking();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
