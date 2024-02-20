namespace WebApp.Data
{
    public class WebApiExecute : IWebApiExecute
    {
        private const string _apiName = "CarsApi";
        private readonly IHttpClientFactory httpClientFactory;

        public WebApiExecute(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(_apiName);
            return await httpClient.GetFromJsonAsync<T>(relativeUrl);
        }
    }
}
