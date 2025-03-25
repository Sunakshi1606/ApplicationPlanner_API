using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityPlannerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherActivityController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IEnumerable<IActivityEvaluator> _activityEvaluators;
        private readonly ILogger<WeatherActivityController> _logger;

        public WeatherActivityController(
            IWeatherService weatherService,
            IEnumerable<IActivityEvaluator> activityEvaluators,
            ILogger<WeatherActivityController> logger)
        {
            _weatherService = weatherService;
            _activityEvaluators = activityEvaluators;
            _logger = logger;
        }        

        [HttpGet("activities/{city}")]
        [ProducesResponseType(typeof(List<ActivityScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ActivityScore>>> GetActivityScoresAsync(string city)
        {
            _logger.LogInformation("Activity scores requested for city: {City}", city);

            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            var activityScores = _activityEvaluators
                .Select(evaluator => evaluator.Evaluate(weatherData))
                .OrderByDescending(score => score.Score)
                .ToList();

            return Ok(activityScores);
        }

        [HttpGet("activity/{activityKey}/{city}")]
        [ProducesResponseType(typeof(ActivityScore), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActivityScore>> GetSpecificActivityScoreAsync(
            [FromServices] IServiceProvider serviceProvider,
            string activityKey,
            string city)
        {
            _logger.LogInformation("Activity score requested for activity: {Activity} in city: {City}",
                activityKey, city);

            var activityEvaluator = serviceProvider.GetKeyedService<IActivityEvaluator>(activityKey);

            if (activityEvaluator == null)
            {
                return NotFound($"Activity '{activityKey}' not found");
            }

            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            var activityScore = activityEvaluator.Evaluate(weatherData);

            return Ok(activityScore);
        }
    }
}