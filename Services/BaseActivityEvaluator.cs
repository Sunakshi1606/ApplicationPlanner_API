using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Models;

namespace ActivityPlannerAPI.Services
{
    public abstract class BaseActivityEvaluator : IActivityEvaluator
    {
        public abstract string ActivityName { get; }
        public abstract string ActivityKey { get; }
        public abstract string ImageUrl { get; }

        public ActivityScore Evaluate(WeatherData weatherData)
        {
            var dailyScores = new List<DailyScore>();

            for (int i = 0; i < weatherData.Daily.Time.Length; i++)
            {
                dailyScores.Add(EvaluateDay(weatherData, i));
            }

            var avgScore = (int)Math.Round(dailyScores.Average(d => d.Score));
            var bestDay = dailyScores.OrderByDescending(d => d.Score).First();

            return new ActivityScore
            {
                Activity = ActivityName,
                Score = avgScore,
                Reason = $"Best day: {FormatDate(bestDay.Date)} ({bestDay.Conditions})",
                Image = ImageUrl,
                DailyScores = dailyScores
            };
        }

        protected abstract DailyScore EvaluateDay(WeatherData weatherData, int dayIndex);

        protected string FormatDate(string date)
        {
            var parsedDate = DateTime.Parse(date);
            return parsedDate.ToString("ddd, MMM d");
        }
    }
}
