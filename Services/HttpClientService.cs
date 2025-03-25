using ActivityPlannerAPI.Exceptions;
using ActivityPlannerAPI.Interface;
using System.Net;
using System.Text.Json;

namespace ActivityPlannerAPI.Services
{
    public class HttpClientService : IHttpClientService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpClientService(HttpClient httpClient, ILogger<HttpClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, _jsonOptions);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("API Error: {StatusCode} - {Content}", (int)response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException($"Resource not found: {requestUri}");
                }

                throw new ApiException($"API Error: {response.StatusCode}", (int)response.StatusCode);
            }
            catch (Exception ex) when (ex is not BaseException)
            {
                _logger.LogError(ex, "HTTP Request Error for {RequestUri}", requestUri);
                throw new ApiException("Failed to complete HTTP request", 500, ex);
            }
        }
    }
}
