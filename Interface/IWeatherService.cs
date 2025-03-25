using ActivityPlannerAPI.Models;

namespace ActivityPlannerAPI.Interface
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync(string city);
        List<WeatherForecast> GetWeatherForecast(WeatherData weatherData);
    }
}
