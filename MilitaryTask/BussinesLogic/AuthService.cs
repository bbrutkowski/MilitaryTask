using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
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
        private readonly IHttpService _httpService;

        private const string ClientId = "f758033d9f0e4c339ad56670d86b5fef";
        private const string ClientSecret = "IMaXLIKaisnPxgWJhARMnK90bvuXqHCcXKKwkiTyXj0dgdBpqceIlMW4eXnKh3Pi";
        private const string _authTokenUrl = "https://allegro.pl/auth/oauth/token";
        private const string _initialAuthUrl = "https://allegro.pl/auth/oauth/device";

        public AuthService(IHttpService httpService) => _httpService = httpService;

        public async Task<Result<string>> GetAuthAsync()
        {
            try
            {
                var authTokenResult = await GetAuthTokenAsync(_authTokenUrl, _initialAuthUrl);
                if (authTokenResult.IsFailure) return Result.Failure<string>(authTokenResult.Error); 

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authTokenResult.Value);
                //var response = await httpClient.GetAsync(url);
                //if (!response.IsSuccessStatusCode) return Result.Failure<string>("Error ocurred while downloading data");

                //var content = await response.Content.ReadAsStringAsync();
                return Result.Success(authTokenResult.Value);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public async Task<Result<string>> GetAuthTokenAsync(string tokenUrl, string initialAuthUrl)
        {
            try
            {
                var deviceCode = await GetInitialDeviceCodeAsync(initialAuthUrl);
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

                var response = await _httpService.SendGetRequest(request);
                if (response.IsFailure) return Result.Failure<string>("Access token not received");

                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Value);
                if (tokenResponse is null) return Result.Failure<string>("Error deserializing access token");

                return Result.Success(tokenResponse.AccessToken);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error occurred while obtaining the access token");
            }
        }

        private async Task<Result<DeviceCodeResponse>> GetInitialDeviceCodeAsync(string initialAuthUrl)
        {
            try
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                var request = new HttpRequestMessage(HttpMethod.Post, initialAuthUrl)
                {
                    Content = new StringContent($"client_id={ClientId}", Encoding.UTF8, "application/x-www-form-urlencoded"),
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await _httpService.SendGetRequest(request);
                if (response.IsFailure) return Result.Failure<DeviceCodeResponse>("Device number not received");

                var deviceCodeResult = JsonConvert.DeserializeObject<DeviceCodeResponse>(response.Value);
                if (deviceCodeResult is null) return Result.Failure<DeviceCodeResponse>("Error deserializing device code");

                return Result.Success(deviceCodeResult);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error occurred while receiving the device code");
            }
        }
    }
}
