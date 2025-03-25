using ActivityPlannerAPI.Models;
using ActivityPlannerAPI.Settings;
using Microsoft.Extensions.Options;

namespace ActivityPlannerAPI.Services
{
    public class OutdoorSightseeingEvaluator : BaseActivityEvaluator
    {
        private readonly ActivityImages _activityImages;

        public OutdoorSightseeingEvaluator(IOptions<ActivityImages> activityImages)
        {
            _activityImages = activityImages.Value;
        }

        public override string ActivityName => "Outdoor Sightseeing";
        public override string ActivityKey => "outdoorSightseeing";
        public override string ImageUrl => _activityImages.OutdoorSightseeing;

        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
        {
            var precip = weatherData.Daily.Precipitation_sum[dayIndex];
            var temp = weatherData.Daily.Temperature_2m_max[dayIndex];
            var date = weatherData.Daily.Time[dayIndex];

            int score;
            string conditions;

            if (precip < 1 && temp > 15 && temp < 30)
            {
                score = 95;
                conditions = "Perfect weather!";
            }
            else if (precip < 3)
            {
                score = 70;
                conditions = "Good conditions";
            }
            else
            {
                score = 40;
                conditions = "Rain expected";
            }

            return new DailyScore
            {
                Date = date,
                Score = score,
                Conditions = conditions
            };
        }
    }
}
