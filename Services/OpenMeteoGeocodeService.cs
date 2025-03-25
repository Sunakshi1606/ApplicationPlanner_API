using ActivityPlannerAPI.Exceptions;
using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Models;
using ActivityPlannerAPI.Settings;
using Microsoft.Extensions.Options;

namespace ActivityPlannerAPI.Services
{
    public class OpenMeteoGeocodeService : IGeocodeService
    {
        private readonly IHttpClientService _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly ILogger<OpenMeteoGeocodeService> _logger;

        public OpenMeteoGeocodeService(
            IHttpClientService httpClient,
            IOptions<ApiSettings> apiSettings,
            ILogger<OpenMeteoGeocodeService> logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _logger = logger;
        }

        public async Task<GeocodingResult> GetCoordinatesAsync(string city)
        {
            try
            {
                _logger.LogInformation("Getting coordinates for city: {City}", city);

                var requestUri = $"{_apiSettings.OpenMeteoGeocodeBaseUrl}/search?name={Uri.EscapeDataString(city)}&count=1&language=en&format=json";
                var result = await _httpClient.GetAsync<GeocodingResponse>(requestUri);

                if (result?.Results == null || !result.Results.Any())
                {
                    throw new NotFoundException($"City not found: {city}");
                }

                var searchedCity = result.Results.FirstOrDefault();
                _logger.LogInformation("Found coordinates for {City}: ({Lat}, {Lon})",
                    city, searchedCity.Latitude, searchedCity.Longitude);

                return searchedCity;
            }
            catch (Exception ex) when (ex is not BaseException)
            {
                _logger.LogError(ex, "Error getting coordinates for city: {City}", city);
                throw;
            }
        }
    }
}
