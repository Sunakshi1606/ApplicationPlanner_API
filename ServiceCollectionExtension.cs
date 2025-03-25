using ActivityPlannerAPI.Interface;
using ActivityPlannerAPI.Services;
using ActivityPlannerAPI.Settings;


namespace ActivityPlannerAPI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
            services.Configure<ActivityImages>(configuration.GetSection("ActivityImages"));

            // Register HTTP client service
            services.AddHttpClient<IHttpClientService, HttpClientService>();

            // Register services
            services.AddScoped<IGeocodeService, OpenMeteoGeocodeService>();
            services.AddScoped<IWeatherService, OpenMeteoWeatherService>();

            // Register activity evaluators with keyed services
            services.AddKeyedScoped<IActivityEvaluator, SkiingEvaluator>("skiing");
            services.AddKeyedScoped<IActivityEvaluator, SurfingEvaluator>("surfing");
            services.AddKeyedScoped<IActivityEvaluator, OutdoorSightseeingEvaluator>("outdoorSightseeing");
            services.AddKeyedScoped<IActivityEvaluator, IndoorSightseeingEvaluator>("indoorSightseeing");

            // Also register them as a collection
            services.AddScoped<IEnumerable<IActivityEvaluator>>(sp =>
            {
                return new List<IActivityEvaluator>
                {
                    sp.GetKeyedService<IActivityEvaluator>("skiing"),
                    sp.GetKeyedService<IActivityEvaluator>("surfing"),
                    sp.GetKeyedService<IActivityEvaluator>("outdoorSightseeing"),
                    sp.GetKeyedService<IActivityEvaluator>("indoorSightseeing")
                };
            });

            return services;
        }
    }

}

