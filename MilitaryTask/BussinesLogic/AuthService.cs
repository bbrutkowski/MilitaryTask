using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model.Auth;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using DeviceCodeResponse = MilitaryTask.Model.Auth.DeviceCodeResponse;

namespace MilitaryTask.BussinesLogic
{
    internal class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string ClientId = "f758033d9f0e4c339ad56670d86b5fef";
        private const string ClientSecret = "IMaXLIKaisnPxgWJhARMnK90bvuXqHCcXKKwkiTyXj0dgdBpqceIlMW4eXnKh3Pi";

        public AuthService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<Result<string>> DownloadDataAsync(string url, string authTokenUrl, string initialAuthUrl)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var authToken = await GetAuthTokenAsync(authTokenUrl, initialAuthUrl, httpClient);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Value);
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return Result.Failure<string>("Error ocurred while downloading data");

                var content = await response.Content.ReadAsStringAsync();
                return Result.Success(content);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error occurred while downloading data"); 
                return Result.Failure<string>(e.Message);
            }
        }

        public async Task<Result<string>> GetAuthTokenAsync(string tokenUrl, string initialAuthUrl, HttpClient httpClient)
        {
            try
            {
                var deviceCode = await GetInitialDeviceCodeAsync(initialAuthUrl, httpClient);
                var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" },
                        { "device_code", deviceCode.Value.DeviceCode }
                    }),
                };

                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode) return Result.Failure<string>("Access token not received");

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                if (tokenResponse is null) return Result.Failure<string>("Error deserializing access token");

                return Result.Success(tokenResponse.AccessToken);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error occurred while obtaining the access token");
                return Result.Failure<string>(e.Message);
            }
        }

        private async Task<Result<DeviceCodeResponse>> GetInitialDeviceCodeAsync(string initialAuthUrl, HttpClient httpClient)
        {
            try
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                var request = new HttpRequestMessage(HttpMethod.Post, initialAuthUrl)
                {
                    Content = new StringContent($"client_id={ClientId}", Encoding.UTF8, "application/x-www-form-urlencoded"),
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode) return Result.Failure<DeviceCodeResponse>("Device number not received");

                var responseContent = await response.Content.ReadAsStringAsync();
                var deviceCodeResult = JsonConvert.DeserializeObject<DeviceCodeResponse>(responseContent);
                if (deviceCodeResult is null) return Result.Failure<DeviceCodeResponse>("Error deserializing device code");

                return Result.Success(deviceCodeResult);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error occurred while receiving the device code");
                return Result.Failure<DeviceCodeResponse>(e.Message);
            }
        }
    }
}
