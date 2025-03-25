using ActivityPlannerAPI.Models;
using ActivityPlannerAPI.Settings;
using Microsoft.Extensions.Options;

namespace ActivityPlannerAPI.Services
{
    public class IndoorSightseeingEvaluator : BaseActivityEvaluator
    {
        private readonly ActivityImages _activityImages;

        public IndoorSightseeingEvaluator(IOptions<ActivityImages> activityImages)
        {
            _activityImages = activityImages.Value;
        }

        public override string ActivityName => "Indoor Sightseeing";
        public override string ActivityKey => "indoorSightseeing";
        public override string ImageUrl => _activityImages.IndoorSightseeing;

        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
        {
            var precip = weatherData.Daily.Precipitation_sum[dayIndex];
            var date = weatherData.Daily.Time[dayIndex];

            int score;
            string conditions;

            if (precip > 5)
            {
                score = 90;
                conditions = "Perfect for indoors!";
            }
            else
            {
                score = 75;
                conditions = "Always an option";
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
