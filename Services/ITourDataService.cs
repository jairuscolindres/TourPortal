namespace TourPortal.Services;

using TourPortal.Models;

public interface ITourDataService
{
    IReadOnlyList<Tour> GetAllTours();
    Tour? GetTourById(string id);
    Task<IReadOnlyList<Tour>> GetAllToursAsync();
    Task<Tour?> GetTourByIdAsync(string id);
}
