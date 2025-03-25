namespace ActivityPlannerAPI.Interface
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}
