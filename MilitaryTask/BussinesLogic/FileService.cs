using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MilitaryTask.BussinesLogic
{
    internal class FileService : IFileService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FileService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<Result<string>> DownloadDataAsync(string url, string authTokenUrl)
        {
            var httpClient = CreateHttpClient();

            try
            {
                var authToken = await GetAuthTokenAsync(authTokenUrl);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Value);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return Result.Success(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
                return Result.Failure<string>(e.Message);
            }
        }

        public async Task<Result<string>> GetAuthTokenAsync(string url)
        {
            var httpClient = CreateHttpClient();

            try
            {
                //var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var postData = new StringContent("grant_type=client_credentials&scope=allegro:api:billing:read", Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await httpClient.PostAsync(url, postData);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject(content);

                return json.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private HttpClient CreateHttpClient() => _httpClientFactory.CreateClient();
    }
}
