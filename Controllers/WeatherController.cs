using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IEnumerable<IActivityEvaluator> _activityEvaluators;
        private readonly ILogger<WeatherActivityController> _logger;

        public WeatherController(IWeatherService weatherService,
           IEnumerable<IActivityEvaluator> activityEvaluators,
            ILogger<WeatherActivityController> logger)
        {
            _weatherService = weatherService;
            _activityEvaluators = activityEvaluators;
            _logger = logger;
        }
       

        [HttpGet("forecast/{city}")]
        [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<WeatherForecast>>> GetWeatherForecastAsync(string city)
        {
            _logger.LogInformation("Weather forecast requested for city: {City}", city);

            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            var forecast = _weatherService.GetWeatherForecast(weatherData);

            return Ok(forecast);
        }       
    }
}
