using ActivityPlannerAPI.Models;

namespace ActivityPlannerAPI.Interface
{
    public interface IGeocodeService
    {
        Task<GeocodingResult> GetCoordinatesAsync(string city);
    }
}
