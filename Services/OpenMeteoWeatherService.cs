using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Models;
using ActivityPlannerAPI.Settings;
using Microsoft.Extensions.Options;

namespace ActivityPlannerAPI.Services
{
    public class OpenMeteoWeatherService : IWeatherService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IGeocodeService _geocodeService;
        private readonly ApiSettings _apiSettings;
        private readonly ILogger<OpenMeteoWeatherService> _logger;

        public OpenMeteoWeatherService(
            IHttpClientService httpClient,
            IGeocodeService geocodeService,
            IOptions<ApiSettings> apiSettings,
            ILogger<OpenMeteoWeatherService> logger)
        {
            _httpClient = httpClient;
            _geocodeService = geocodeService;
            _apiSettings = apiSettings.Value;
            _logger = logger;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            try
            {
                _logger.LogInformation("Getting weather data for city: {City}", city);

                var coordinates = await _geocodeService.GetCoordinatesAsync(city);

                var requestUri = $"{_apiSettings.OpenMeteoBaseUrl}/forecast?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,snowfall_sum,windspeed_10m_max&timezone=auto";
                var weatherData = await _httpClient.GetAsync<WeatherData>(requestUri);

                _logger.LogInformation("Successfully retrieved weather data for {City}", city);
                return weatherData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting weather data for city: {City}", city);
                throw;
            }
        }

        public List<WeatherForecast> GetWeatherForecast(WeatherData weatherData)
        {
            try
            {
                var forecasts = new List<WeatherForecast>();

                for (int i = 0; i < weatherData!.Daily!.Time!.Length; i++) 
                {
                    
                    forecasts.Add(new WeatherForecast
                    {
                        Date = weatherData.Daily.Time[i],
                        MaxTemp = (int)Math.Round(weatherData.Daily.Temperature_2m_max[i]),
                        MinTemp = (int)Math.Round(weatherData.Daily.Temperature_2m_min[i]),
                        Precipitation = Math.Round(weatherData.Daily.Precipitation_sum[i] * 10) / 10,
                        Windspeed = (int)Math.Round(weatherData.Daily.Windspeed_10m_max[i])
                    });
                }

                return forecasts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing weather forecast data");
                throw;
            }
        }
    }
}
