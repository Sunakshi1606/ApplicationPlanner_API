using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoCodeController : ControllerBase
    {
        private readonly IGeocodeService _geocodeService;        
        private readonly ILogger<WeatherActivityController> _logger;

        public GeoCodeController(IGeocodeService geocodeService,
        ILogger<WeatherActivityController> logger)
        {
            _geocodeService = geocodeService;
            _logger = logger;
        }

        [HttpGet("coordinates/{city}")]
        [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GeocodingResponse>>> GetCoordinatesAsync(string city)
        {
            _logger.LogInformation("Coordinate data requested for city: {City}", city);

            var coordintes = await _geocodeService.GetCoordinatesAsync(city);

            return Ok(coordintes);
        }
    }
}
