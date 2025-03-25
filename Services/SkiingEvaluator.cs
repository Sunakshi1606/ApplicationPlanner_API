using ActivityPlannerAPI.Models;
using ActivityPlannerAPI.Settings;
using Microsoft.Extensions.Options;

namespace ActivityPlannerAPI.Services
{
    public class SkiingEvaluator : BaseActivityEvaluator
    {
        private readonly ActivityImages _activityImages;

        public SkiingEvaluator(IOptions<ActivityImages> activityImages)
        {
            _activityImages = activityImages.Value;
        }

        public override string ActivityName => "Skiing";
        public override string ActivityKey => "skiing";
        public override string ImageUrl => _activityImages.Skiing;

        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
        {
            var snowfall = weatherData.Daily.Snowfall_sum[dayIndex];
            var temp = weatherData.Daily.Temperature_2m_max[dayIndex];
            var date = weatherData.Daily.Time[dayIndex];

            int score;
            string conditions;

            if (snowfall > 5 && temp < 5)
            {
                score = 90;
                conditions = "Perfect snow conditions!";
            }
            else if (snowfall > 2)
            {
                score = 70;
                conditions = "Some snow expected";
            }
            else
            {
                score = 20;
                conditions = "Limited snow";
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
