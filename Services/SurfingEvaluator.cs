using ActivityPlannerAPI.Models;
using ActivityPlannerAPI.Settings;
using Microsoft.Extensions.Options;

namespace ActivityPlannerAPI.Services
{
    public class SurfingEvaluator : BaseActivityEvaluator
    {
        private readonly ActivityImages _activityImages;

        public SurfingEvaluator(IOptions<ActivityImages> activityImages)
        {
            _activityImages = activityImages.Value;
        }

        public override string ActivityName => "Surfing";
        public override string ActivityKey => "surfing";
        public override string ImageUrl => _activityImages.Surfing;

        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
        {
            var wind = weatherData.Daily.Windspeed_10m_max[dayIndex];
            var date = weatherData.Daily.Time[dayIndex];

            int score;
            string conditions;

            if (wind > 15 && wind < 30)
            {
                score = 85;
                conditions = "Great wind conditions!";
            }
            else if (wind > 10)
            {
                score = 60;
                conditions = "Moderate surf conditions";
            }
            else
            {
                score = 30;
                conditions = "Low wind for surfing";
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
