using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using System.Net.Http.Headers;
using System.Web;

namespace MilitaryTask.BussinesLogic
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public HttpClient CreateClient() => _httpClientFactory.CreateClient();

        public async Task<Result<string>> GetResponseContentAsync(HttpRequestMessage request, string? bearerToken)
        {
            try
            {
                var response = await SendRequestAsync(request, bearerToken);
                if (!response.IsSuccessStatusCode) return Result.Failure<string>($"Sending the request resulted in a code: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync();
                return Result.Success(content);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public Result<HttpRequestMessage> CreateGetRequestWithParams(string baseUrl, string paramName, string paramValue)
        {
            try
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query[paramName] = paramValue;

                var uriBuilder = new UriBuilder(baseUrl)
                {
                    Query = query.ToString()
                };

                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

                return Result.Success(request);
            }
            catch (Exception ex)
            {
                return Result.Failure<HttpRequestMessage>(ex.Message);
            }
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, string? bearerToken)
        {          
            var httpClient = CreateClient();

            if (!string.IsNullOrEmpty(bearerToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            return await httpClient.SendAsync(request);
        }
    }
}
