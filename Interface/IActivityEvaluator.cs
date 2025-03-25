using ActivityPlannerAPI.Models;

namespace ActivityPlannerAPI.Interface
{
    public interface IActivityEvaluator
    {
        string ActivityName { get; }
        string ActivityKey { get; }
        string ImageUrl { get; }
        ActivityScore Evaluate(WeatherData weatherData);
    }
}
