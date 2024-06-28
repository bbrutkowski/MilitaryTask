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

        public async Task<Result<string>> SendGetRequest(HttpRequestMessage request)
        {
            try
            {
                var client = CreateClient();

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return Result.Success(content);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Error sending request");
            }
        }

        public async Task<Result<string>> SendGetRequestWithBearerToken(HttpRequestMessage request, string bearerToken)
        {
            var client = CreateClient();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) return Result.Failure<string>($"Sending the request resulted in a code: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content)) return Result.Failure<string>(content);
                return Result.Success(content);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Error sending request");
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
            catch (Exception)
            {
                throw new ApplicationException("Error creating url with params");
            }
        }
    }
}
