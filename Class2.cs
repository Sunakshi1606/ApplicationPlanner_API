//// WeatherActivity.Core/Models/WeatherData.cs
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using System.Net;
//using System.Text.Json;
//using WeatherActivity.API.Middleware;
//using WeatherActivity.Core.Exceptions;
//using WeatherActivity.Core.Interfaces;
//using WeatherActivity.Core.Models;
//using WeatherActivity.Core.Services.ActivityEvaluators;
//using WeatherActivity.Core.Settings;
//using WeatherActivity.Infrastructure.Services;
//using WeatherActivity.Infrastructure;

//namespace WeatherActivity.Core.Models
//{
//    //public class WeatherData
//    //{
//    //    public DailyWeatherData Daily { get; set; }

//    //    public class DailyWeatherData
//    //    {
//    //        public string[] Time { get; set; }
//    //        public double[] Temperature_2m_max { get; set; }
//    //        public double[] Temperature_2m_min { get; set; }
//    //        public double[] Precipitation_sum { get; set; }
//    //        public double[] Snowfall_sum { get; set; }
//    //        public double[] Windspeed_10m_max { get; set; }
//    //    }
//    //}
//}

//// WeatherActivity.Core/Models/WeatherForecast.cs
////namespace WeatherActivity.Core.Models
////{
////    public class WeatherForecast
////    {
////        public string Date { get; set; }
////        public int MaxTemp { get; set; }
////        public int MinTemp { get; set; }
////        public double Precipitation { get; set; }
////        public int Windspeed { get; set; }
////    }
////}

//// WeatherActivity.Core/Models/DailyScore.cs
////namespace WeatherActivity.Core.Models
////{
////    public class DailyScore
////    {
////        public string Date { get; set; }
////        public int Score { get; set; }
////        public string Conditions { get; set; }
////    }
////}

//// WeatherActivity.Core/Models/ActivityScore.cs
////namespace WeatherActivity.Core.Models
////{
////    public class ActivityScore
////    {
////        public string Activity { get; set; }
////        public int Score { get; set; }
////        public string Reason { get; set; }
////        public string Image { get; set; }
////        public List<DailyScore> DailyScores { get; set; }
////    }
////}

//// WeatherActivity.Core/Exceptions/BaseException.cs
//namespace WeatherActivity.Core.Exceptions
//{
//public abstract class BaseException : Exception
//{
//    public BaseException(string message) : base(message)
//    {
//    }

//    public BaseException(string message, Exception innerException) : base(message, innerException)
//    {
//    }
//}
//}

//// WeatherActivity.Core/Exceptions/NotFoundException.cs
//namespace WeatherActivity.Core.Exceptions
//{
//public class NotFoundException : BaseException
//{
//    public NotFoundException(string message) : base(message)
//    {
//    }

//    public NotFoundException(string message, Exception innerException) : base(message, innerException)
//    {
//    }
//}
//}

//// WeatherActivity.Core/Exceptions/ApiException.cs
//namespace WeatherActivity.Core.Exceptions
//{
//public class ApiException : BaseException
//{
//    public int StatusCode { get; }

//    public ApiException(string message, int statusCode) : base(message)
//    {
//        StatusCode = statusCode;
//    }

//    public ApiException(string message, int statusCode, Exception innerException) : base(message, innerException)
//    {
//        StatusCode = statusCode;
//    }
//}
//}

//// WeatherActivity.Core/Interfaces/IGeocodeService.cs
////namespace WeatherActivity.Core.Interfaces
////{
////    public interface IGeocodeService
////    {
////        Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string city);
////    }
////}

//// WeatherActivity.Core/Interfaces/IWeatherService.cs
////using WeatherActivity.Core.Models;

////namespace WeatherActivity.Core.Interfaces
////{
////    public interface IWeatherService
////    {
////        Task<WeatherData> GetWeatherDataAsync(string city);
////        List<WeatherForecast> GetWeatherForecast(WeatherData weatherData);
////    }
////}

//// WeatherActivity.Core/Interfaces/IActivityEvaluator.cs
////using WeatherActivity.Core.Models;

////namespace WeatherActivity.Core.Interfaces
////{
////    public interface IActivityEvaluator
////    {
////        string ActivityName { get; }
////        string ActivityKey { get; }
////        string ImageUrl { get; }
////        ActivityScore Evaluate(WeatherData weatherData);
////    }
////}

//// WeatherActivity.Core/Interfaces/IHttpClientService.cs
////namespace WeatherActivity.Core.Interfaces
////{
////    public interface IHttpClientService
////    {
////        Task<T> GetAsync<T>(string requestUri);
////    }
////}

//// WeatherActivity.Core/Services/BaseActivityEvaluator.cs
////using WeatherActivity.Core.Interfaces;
////using WeatherActivity.Core.Models;

////namespace WeatherActivity.Core.Services
////{
////    public abstract class BaseActivityEvaluator : IActivityEvaluator
////    {
////        public abstract string ActivityName { get; }
////        public abstract string ActivityKey { get; }
////        public abstract string ImageUrl { get; }

////        public ActivityScore Evaluate(WeatherData weatherData)
////        {
////            var dailyScores = new List<DailyScore>();

////            for (int i = 0; i < weatherData.Daily.Time.Length; i++)
////            {
////                dailyScores.Add(EvaluateDay(weatherData, i));
////            }

////            var avgScore = (int)Math.Round(dailyScores.Average(d => d.Score));
////            var bestDay = dailyScores.OrderByDescending(d => d.Score).First();

////            return new ActivityScore
////            {
////                Activity = ActivityName,
////                Score = avgScore,
////                Reason = $"Best day: {FormatDate(bestDay.Date)} ({bestDay.Conditions})",
////                Image = ImageUrl,
////                DailyScores = dailyScores
////            };
////        }

////        protected abstract DailyScore EvaluateDay(WeatherData weatherData, int dayIndex);

////        protected string FormatDate(string date)
////        {
////            var parsedDate = DateTime.Parse(date);
////            return parsedDate.ToString("ddd, MMM d");
////        }
////    }
////}

//// WeatherActivity.Core/Services/ActivityEvaluators/SkiingEvaluator.cs
////using Microsoft.Extensions.Options;
////using WeatherActivity.Core.Models;
////using WeatherActivity.Core.Settings;

////namespace WeatherActivity.Core.Services.ActivityEvaluators
////{
////    public class SkiingEvaluator : BaseActivityEvaluator
////    {
////        private readonly ActivityImages _activityImages;

////        public SkiingEvaluator(IOptions<ActivityImages> activityImages)
////        {
////            _activityImages = activityImages.Value;
////        }

////        public override string ActivityName => "Skiing";
////        public override string ActivityKey => "skiing";
////        public override string ImageUrl => _activityImages.Skiing;

////        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
////        {
////            var snowfall = weatherData.Daily.Snowfall_sum[dayIndex];
////            var temp = weatherData.Daily.Temperature_2m_max[dayIndex];
////            var date = weatherData.Daily.Time[dayIndex];

////            int score;
////            string conditions;

////            if (snowfall > 5 && temp < 5)
////            {
////                score = 90;
////                conditions = "Perfect snow conditions!";
////            }
////            else if (snowfall > 2)
////            {
////                score = 70;
////                conditions = "Some snow expected";
////            }
////            else
////            {
////                score = 20;
////                conditions = "Limited snow";
////            }

////            return new DailyScore
////            {
////                Date = date,
////                Score = score,
////                Conditions = conditions
////            };
////        }
////    }
////}

//// WeatherActivity.Core/Services/ActivityEvaluators/SurfingEvaluator.cs
////using Microsoft.Extensions.Options;
////using WeatherActivity.Core.Models;
////using WeatherActivity.Core.Settings;

////namespace WeatherActivity.Core.Services.ActivityEvaluators
////{
////    public class SurfingEvaluator : BaseActivityEvaluator
////    {
////        private readonly ActivityImages _activityImages;

////        public SurfingEvaluator(IOptions<ActivityImages> activityImages)
////        {
////            _activityImages = activityImages.Value;
////        }

////        public override string ActivityName => "Surfing";
////        public override string ActivityKey => "surfing";
////        public override string ImageUrl => _activityImages.Surfing;

////        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
////        {
////            var wind = weatherData.Daily.Windspeed_10m_max[dayIndex];
////            var date = weatherData.Daily.Time[dayIndex];

////            int score;
////            string conditions;

////            if (wind > 15 && wind < 30)
////            {
////                score = 85;
////                conditions = "Great wind conditions!";
////            }
////            else if (wind > 10)
////            {
////                score = 60;
////                conditions = "Moderate surf conditions";
////            }
////            else
////            {
////                score = 30;
////                conditions = "Low wind for surfing";
////            }

////            return new DailyScore
////            {
////                Date = date,
////                Score = score,
////                Conditions = conditions
////            };
////        }
////    }
////}

//// WeatherActivity.Core/Services/ActivityEvaluators/OutdoorSightseeingEvaluator.cs
////using Microsoft.Extensions.Options;
////using WeatherActivity.Core.Models;
////using WeatherActivity.Core.Settings;

////namespace WeatherActivity.Core.Services.ActivityEvaluators
////{
////    public class OutdoorSightseeingEvaluator : BaseActivityEvaluator
////    {
////        private readonly ActivityImages _activityImages;

////        public OutdoorSightseeingEvaluator(IOptions<ActivityImages> activityImages)
////        {
////            _activityImages = activityImages.Value;
////        }

////        public override string ActivityName => "Outdoor Sightseeing";
////        public override string ActivityKey => "outdoorSightseeing";
////        public override string ImageUrl => _activityImages.OutdoorSightseeing;

////        protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
////        {
////            var precip = weatherData.Daily.Precipitation_sum[dayIndex];
////            var temp = weatherData.Daily.Temperature_2m_max[dayIndex];
////            var date = weatherData.Daily.Time[dayIndex];

////            int score;
////            string conditions;

////            if (precip < 1 && temp > 15 && temp < 30)
////            {
////                score = 95;
////                conditions = "Perfect weather!";
////            }
////            else if (precip < 3)
////            {
////                score = 70;
////                conditions = "Good conditions";
////            }
////            else
////            {
////                score = 40;
////                conditions = "Rain expected";
////            }

////            return new DailyScore
////            {
////                Date = date,
////                Score = score,
////                Conditions = conditions
////            };
////        }
////    }
////}

//// WeatherActivity.Core/Services/ActivityEvaluators/IndoorSightseeingEvaluator.cs
////using Microsoft.Extensions.Options;
////using WeatherActivity.Core.Models;
////using WeatherActivity.Core.Settings;

////namespace WeatherActivity.Core.Services.ActivityEvaluators
////{
//    //public class IndoorSightseeingEvaluator : BaseActivityEvaluator
//    //{
//    //    private readonly ActivityImages _activityImages;

//    //    public IndoorSightseeingEvaluator(IOptions<ActivityImages> activityImages)
//    //    {
//    //        _activityImages = activityImages.Value;
//    //    }

//    //    public override string ActivityName => "Indoor Sightseeing";
//    //    public override string ActivityKey => "indoorSightseeing";
//    //    public override string ImageUrl => _activityImages.IndoorSightseeing;

//    //    protected override DailyScore EvaluateDay(WeatherData weatherData, int dayIndex)
//    //    {
//    //        var precip = weatherData.Daily.Precipitation_sum[dayIndex];
//    //        var date = weatherData.Daily.Time[dayIndex];

//    //        int score;
//    //        string conditions;

//    //        if (precip > 5)
//    //        {
//    //            score = 90;
//    //            conditions = "Perfect for indoors!";
//    //        }
//    //        else
//    //        {
//    //            score = 75;
//    //            conditions = "Always an option";
//    //        }

//    //        return new DailyScore
//    //        {
//    //            Date = date,
//    //            Score = score,
//    //            Conditions = conditions
//    //        };
//    //    }
//    //}
////}

//// WeatherActivity.Core/Settings/ApiSettings.cs
////namespace WeatherActivity.Core.Settings
////{
////    public class ApiSettings
////    {
////        public string OpenMeteoBaseUrl { get; set; }
////        public string OpenMeteoGeocodeBaseUrl { get; set; }
////    }
////}

//// WeatherActivity.Core/Settings/ActivityImages.cs
////namespace WeatherActivity.Core.Settings
////{
////    public class ActivityImages
////    {
////        public string Skiing { get; set; }
////        public string Surfing { get; set; }
////        public string OutdoorSightseeing { get; set; }
////        public string IndoorSightseeing { get; set; }
////    }
////}

//// WeatherActivity.Infrastructure/HttpClients/Models/GeocodingResponse.cs
////namespace WeatherActivity.Infrastructure.HttpClients.Models
////{
////    public class GeocodingResponse
////    {
////        public List<GeocodingResult> Results { get; set; }
////    }

////    public class GeocodingResult
////    {
////        public double Latitude { get; set; }
////        public double Longitude { get; set; }
////        public string Name { get; set; }
////        public string Country { get; set; }
////    }
////}

//// WeatherActivity.Infrastructure/Services/HttpClientService.cs
////using System.Net;
////using System.Text.Json;
////using Microsoft.Extensions.Logging;
////using WeatherActivity.Core.Exceptions;
////using WeatherActivity.Core.Interfaces;

////namespace WeatherActivity.Infrastructure.Services
////{
////    public class HttpClientService : IHttpClientService
////    {
////        private readonly HttpClient _httpClient;
////        private readonly ILogger<HttpClientService> _logger;
////        private readonly JsonSerializerOptions _jsonOptions;

////        public HttpClientService(HttpClient httpClient, ILogger<HttpClientService> logger)
////        {
////            _httpClient = httpClient;
////            _logger = logger;
////            _jsonOptions = new JsonSerializerOptions
////            {
////                PropertyNameCaseInsensitive = true
////            };
////        }

////        public async Task<T> GetAsync<T>(string requestUri)
////        {
////            try
////            {
////                var response = await _httpClient.GetAsync(requestUri);

////                if (response.IsSuccessStatusCode)
////                {
////                    var content = await response.Content.ReadAsStringAsync();
////                    return JsonSerializer.Deserialize<T>(content, _jsonOptions);
////                }

////                var errorContent = await response.Content.ReadAsStringAsync();
////                _logger.LogError("API Error: {StatusCode} - {Content}", (int)response.StatusCode, errorContent);

////                if (response.StatusCode == HttpStatusCode.NotFound)
////                {
////                    throw new NotFoundException($"Resource not found: {requestUri}");
////                }

////                throw new ApiException($"API Error: {response.StatusCode}", (int)response.StatusCode);
////            }
////            catch (Exception ex) when (ex is not BaseException)
////            {
////                _logger.LogError(ex, "HTTP Request Error for {RequestUri}", requestUri);
////                throw new ApiException("Failed to complete HTTP request", 500, ex);
////            }
////        }
////    }
////}

//// WeatherActivity.Infrastructure/Services/OpenMeteoGeocodeService.cs
////using Microsoft.Extensions.Logging;
////using Microsoft.Extensions.Options;
////using WeatherActivity.Core.Exceptions;
////using WeatherActivity.Core.Interfaces;
////using WeatherActivity.Core.Settings;
////using WeatherActivity.Infrastructure.HttpClients.Models;

////namespace WeatherActivity.Infrastructure.Services
////{
////    public class OpenMeteoGeocodeService : IGeocodeService
////    {
////        private readonly IHttpClientService _httpClient;
////        private readonly ApiSettings _apiSettings;
////        private readonly ILogger<OpenMeteoGeocodeService> _logger;

////        public OpenMeteoGeocodeService(
////            IHttpClientService httpClient,
////            IOptions<ApiSettings> apiSettings,
////            ILogger<OpenMeteoGeocodeService> logger)
////        {
////            _httpClient = httpClient;
////            _apiSettings = apiSettings.Value;
////            _logger = logger;
////        }

////        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string city)
////        {
////            try
////            {
////                _logger.LogInformation("Getting coordinates for city: {City}", city);

////                var requestUri = $"{_apiSettings.OpenMeteoGeocodeBaseUrl}/search?name={Uri.EscapeDataString(city)}&count=1&language=en&format=json";
////                var result = await _httpClient.GetAsync<GeocodingResponse>(requestUri);

////                if (result?.Results == null || !result.Results.Any())
////                {
////                    throw new NotFoundException($"City not found: {city}");
////                }

////                var coordinates = (result.Results[0].Latitude, result.Results[0].Longitude);
////                _logger.LogInformation("Found coordinates for {City}: ({Lat}, {Lon})",
////                    city, coordinates.Latitude, coordinates.Longitude);

////                return coordinates;
////            }
////            catch (Exception ex) when (ex is not BaseException)
////            {
////                _logger.LogError(ex, "Error getting coordinates for city: {City}", city);
////                throw;
////            }
////        }
////    }
////}

//// WeatherActivity.Infrastructure/Services/OpenMeteoWeatherService.cs
////using Microsoft.Extensions.Logging;
////using Microsoft.Extensions.Options;
////using WeatherActivity.Core.Interfaces;
////using WeatherActivity.Core.Models;
////using WeatherActivity.Core.Settings;

////namespace WeatherActivity.Infrastructure.Services
////{
////    public class OpenMeteoWeatherService : IWeatherService
////    {
////        private readonly IHttpClientService _httpClient;
////        private readonly IGeocodeService _geocodeService;
////        private readonly ApiSettings _apiSettings;
////        private readonly ILogger<OpenMeteoWeatherService> _logger;

////        public OpenMeteoWeatherService(
////            IHttpClientService httpClient,
////            IGeocodeService geocodeService,
////            IOptions<ApiSettings> apiSettings,
////            ILogger<OpenMeteoWeatherService> logger)
////        {
////            _httpClient = httpClient;
////            _geocodeService = geocodeService;
////            _apiSettings = apiSettings.Value;
////            _logger = logger;
////        }

////        public async Task<WeatherData> GetWeatherDataAsync(string city)
////        {
////            try
////            {
////                _logger.LogInformation("Getting weather data for city: {City}", city);

////                var (latitude, longitude) = await _geocodeService.GetCoordinatesAsync(city);

////                var requestUri = $"{_apiSettings.OpenMeteoBaseUrl}/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,snowfall_sum,windspeed_10m_max&timezone=auto";
////                var weatherData = await _httpClient.GetAsync<WeatherData>(requestUri);

////                _logger.LogInformation("Successfully retrieved weather data for {City}", city);
////                return weatherData;
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Error getting weather data for city: {City}", city);
////                throw;
////            }
////        }

////        public List<WeatherForecast> GetWeatherForecast(WeatherData weatherData)
////        {
////            try
////            {
////                var forecasts = new List<WeatherForecast>();

////                for (int i = 0; i < weatherData.Daily.Time.Length; i++)
////                {
////                    forecasts.Add(new WeatherForecast
////                    {
////                        Date = weatherData.Daily.Time[i],
////                        MaxTemp = (int)Math.Round(weatherData.Daily.Temperature_2m_max[i]),
////                        MinTemp = (int)Math.Round(weatherData.Daily.Temperature_2m_min[i]),
////                        Precipitation = Math.Round(weatherData.Daily.Precipitation_sum[i] * 10) / 10,
////                        Windspeed = (int)Math.Round(weatherData.Daily.Windspeed_10m_max[i])
////                    });
////                }

////                return forecasts;
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Error processing weather forecast data");
////                throw;
////            }
////        }
////    }
////}

//// WeatherActivity.Infrastructure/DependencyInjection.cs
////using Microsoft.Extensions.Configuration;
////using Microsoft.Extensions.DependencyInjection;
////using WeatherActivity.Core.Interfaces;
////using WeatherActivity.Core.Services.ActivityEvaluators;
////using WeatherActivity.Core.Settings;
////using WeatherActivity.Infrastructure.Services;

////namespace WeatherActivity.Infrastructure
////{
////    public static class DependencyInjection
////    {
////        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
////        {
////            // Register configuration
////            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
////            services.Configure<ActivityImages>(configuration.GetSection("ActivityImages"));

////            // Register HTTP client service
////            services.AddHttpClient<IHttpClientService, HttpClientService>();

////            // Register services
////            services.AddScoped<IGeocodeService, OpenMeteoGeocodeService>();
////            services.AddScoped<IWeatherService, OpenMeteoWeatherService>();

////            // Register activity evaluators with keyed services
////            services.AddKeyedScoped<IActivityEvaluator, SkiingEvaluator>("skiing");
////            services.AddKeyedScoped<IActivityEvaluator, SurfingEvaluator>("surfing");
////            services.AddKeyedScoped<IActivityEvaluator, OutdoorSightseeingEvaluator>("outdoorSightseeing");
////            services.AddKeyedScoped<IActivityEvaluator, IndoorSightseeingEvaluator>("indoorSightseeing");

////            // Also register them as a collection
////            services.AddScoped<IEnumerable<IActivityEvaluator>>(sp =>
////            {
////                return new List<IActivityEvaluator>
////                {
////                    sp.GetKeyedService<IActivityEvaluator>("skiing"),
////                    sp.GetKeyedService<IActivityEvaluator>("surfing"),
////                    sp.GetKeyedService<IActivityEvaluator>("outdoorSightseeing"),
////                    sp.GetKeyedService<IActivityEvaluator>("indoorSightseeing")
////                };
////            });

////            return services;
////        }
////    }
////}

//// WeatherActivity.API/Middleware/ExceptionHandlingMiddleware.cs
////using System.Net;
////using System.Text.Json;
////using WeatherActivity.Core.Exceptions;

////namespace WeatherActivity.API.Middleware
////{
////    public class ExceptionHandlingMiddleware
////    {
////        private readonly RequestDelegate _next;
////        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

////        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
////        {
////            _next = next;
////            _logger = logger;
////        }

////        public async Task InvokeAsync(HttpContext context)
////        {
////            try
////            {
////                await _next(context);
////            }
////            catch (Exception ex)
////            {
////                await HandleExceptionAsync(context, ex);
////            }
////        }

////        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
////        {
////            _logger.LogError(exception, "An unhandled exception occurred");

////            var statusCode = GetStatusCode(exception);
////            var response = new
////            {
////                status = statusCode,
////                message = GetMessage(exception),
////                detail = exception.StackTrace
////            };

////            context.Response.ContentType = "application/json";
////            context.Response.StatusCode = statusCode;

////            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
////        }

////        private static int GetStatusCode(Exception exception)
////        {
////            return exception switch
////            {
////                NotFoundException => (int)HttpStatusCode.NotFound,
////                ApiException apiException => apiException.StatusCode,
////                _ => (int)HttpStatusCode.InternalServerError
////            };
////        }

////        private static string GetMessage(Exception exception)
////        {
////            return exception switch
////            {
////                BaseException => exception.Message,
////                _ => "An unexpected error occurred"
////            };
////        }
////    }

////    // Extension method for middleware registration
////    public static class ExceptionHandlingMiddlewareExtensions
////    {
////        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
////        {
////            return app.UseMiddleware<ExceptionHandlingMiddleware>();
////        }
////    }
////}

//// WeatherActivity.API/Controllers/WeatherActivityController.cs
////using Microsoft.AspNetCore.Mvc;
////using WeatherActivity.Core.Interfaces;
////using WeatherActivity.Core.Models;

////namespace WeatherActivity.API.Controllers
////{
////    [ApiController]
////    [Route("api/[controller]")]
////    public class WeatherActivityController : ControllerBase
////    {
////        private readonly IWeatherService _weatherService;
////        private readonly IEnumerable<IActivityEvaluator> _activityEvaluators;
////        private readonly ILogger<WeatherActivityController> _logger;

////        public WeatherActivityController(
////            IWeatherService weatherService,
////            IEnumerable<IActivityEvaluator> activityEvaluators,
////            ILogger<WeatherActivityController> logger)
////        {
////            _weatherService = weatherService;
////            _activityEvaluators = activityEvaluators;
////            _logger = logger;
////        }

////        [HttpGet("forecast/{city}")]
////        [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
////        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
////        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
////        public async Task<ActionResult<List<WeatherForecast>>> GetWeatherForecast(string city)
////        {
////            _logger.LogInformation("Weather forecast requested for city: {City}", city);

////            var weatherData = await _weatherService.GetWeatherDataAsync(city);
////            var forecast = _weatherService.GetWeatherForecast(weatherData);

////            return Ok(forecast);
////        }

////        [HttpGet("activities/{city}")]
////        [ProducesResponseType(typeof(List<ActivityScore>), StatusCodes.Status200OK)]
////        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
////        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
////        public async Task<ActionResult<List<ActivityScore>>> GetActivityScores(string city)
////        {
////            _logger.LogInformation("Activity scores requested for city: {City}", city);

////            var weatherData = await _weatherService.GetWeatherDataAsync(city);
////            var activityScores = _activityEvaluators
////                .Select(evaluator => evaluator.Evaluate(weatherData))
////                .OrderByDescending(score => score.Score)
////                .ToList();

////            return Ok(activityScores);
////        }

////        [HttpGet("activity/{activityKey}/{city}")]
////        [ProducesResponseType(typeof(ActivityScore), StatusCodes.Status200OK)]
////        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
////        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
////        public async Task<ActionResult<ActivityScore>> GetSpecificActivityScore(
////            [FromServices] IServiceProvider serviceProvider,
////            string activityKey,
////            string city)
////        {
////            _logger.LogInformation("Activity score requested for activity: {Activity} in city: {City}",
////                activityKey, city);

////            var activityEvaluator = serviceProvider.GetKeyedService<IActivityEvaluator>(activityKey);

////            if (activityEvaluator == null)
////            {
////                return NotFound($"Activity '{activityKey}' not found");
////            }

////            var weatherData = await _weatherService.GetWeatherDataAsync(city);
////            var activityScore = activityEvaluator.Evaluate(weatherData);

////            return Ok(activityScore);
////        }
////    }
////}

//// WeatherActivity.API/Program.cs
//using WeatherActivity.API.Middleware;
//using WeatherActivity.Infrastructure;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Add application services
//builder.Services.AddInfrastructure(builder.Configuration);

//// CORS policy
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseExceptionHandling(); // Custom middleware
//app.UseCors("AllowAll");
//app.UseAuthorization();
//app.MapControllers();

//app.Run();

//// WeatherActivity.API/appsettings.json
////{
////    "Logging": {
////        "LogLevel": {
////            "Default": "Information",
////      "Microsoft.AspNetCore": "Warning"
////        }
////    },
////  "AllowedHosts": "*",
////  "ApiSettings": {
////        "OpenMeteoBaseUrl": "https://api.open-meteo.com/v1",
////    "OpenMeteoGeocodeBaseUrl": "https://geocoding-api.open-meteo.com/v1"
////  },
////  "ActivityImages": {
////        "Skiing": "https://images.unsplash.com/photo-1551698618-1dfe5d97d256?w=800&auto=format&fit=crop",
////    "Surfing": "https://images.unsplash.com/photo-1502680390469-be75c86b636f?w=800&auto=format&fit=crop",
////    "OutdoorSightseeing": "https://images.unsplash.com/photo-1533587851505-d119e13fa0d7?w=800&auto=format&fit=crop",
////    "IndoorSightseeing": "https://offloadmedia.feverup.com/secretldn.com/wp-content/uploads/2018/10/22055057/Museums-1024x683.jpg"
////  }
////}

//// WeatherActivity.API/WeatherActivity.API.csproj
//< Project Sdk = "Microsoft.NET.Sdk.Web" >

//  < PropertyGroup >
//    < TargetFramework > net7.0 </ TargetFramework >
//    < Nullable > enable </ Nullable >
//    < ImplicitUsings > enable </ ImplicitUsings >
//  </ PropertyGroup >

//  < ItemGroup >
//    < PackageReference Include = "Microsoft.AspNetCore.OpenApi" Version = "7.0.9" />
//    < PackageReference Include = "Swashbuckle.AspNetCore" Version = "6.5.0" />
//  </ ItemGroup >

//  < ItemGroup >
//    < ProjectReference Include = "..\WeatherActivity.Core\WeatherActivity.Core.csproj" />
//    < ProjectReference Include = "..\WeatherActivity.Infrastructure\WeatherActivity.Infrastructure.csproj" />
//  </ ItemGroup >

//</ Project >

//// WeatherActivity.Core/WeatherActivity.Core.csproj
//< Project Sdk = "Microsoft.NET.Sdk" >

//  < PropertyGroup >
//    < TargetFramework > net7.0 </ Targ