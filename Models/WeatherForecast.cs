namespace ActivityPlannerAPI.Models
{
    public class WeatherForecast
    {
        public required string Date { get; set; }
        public int MaxTemp { get; internal set; }
        public int MinTemp { get; set; }
        public double Precipitation { get; set; }
        public int Windspeed { get; set; }
    }
}
