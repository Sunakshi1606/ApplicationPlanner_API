namespace ActivityPlannerAPI.Models
{
    public class WeatherData
    {
        public required DailyWeatherData Daily { get; set; }

        public class DailyWeatherData
        {
            public string[]? Time { get; set; }
            public double[]? Temperature_2m_max { get; set; }
            public double[]? Temperature_2m_min { get; set; }
            public double[]? Precipitation_sum { get; set; }
            public double[]? Snowfall_sum { get; set; }
            public double[]? Windspeed_10m_max { get; set; }
        }
    }
}
