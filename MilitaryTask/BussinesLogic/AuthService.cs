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
        private readonly IHttpService _httpService;

        private const string _clientId = "f758033d9f0e4c339ad56670d86b5fef";
        private const string _clientSecret = "IMaXLIKaisnPxgWJhARMnK90bvuXqHCcXKKwkiTyXj0dgdBpqceIlMW4eXnKh3Pi";
        private const string _authTokenUrl = "https://allegro.pl/auth/oauth/token";
        private const string _initialAuthUrl = "https://allegro.pl/auth/oauth/device";

        public AuthService(IHttpService httpService) => _httpService = httpService;

        public async Task<Result<string>> GetAuthAsync()
        {
            try
            {
                var deviceCodeResult = await GetInitialDeviceCodeAsync(_initialAuthUrl);
                if (deviceCodeResult.IsFailure) return Result.Failure<string>(deviceCodeResult.Error);

                var authTokenResult = await GetAuthTokenAsync(deviceCodeResult.Value.DeviceCode);               
                return Result.Success(authTokenResult.Value);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public async Task<Result<string>> GetAuthTokenAsync(string deviceCode)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl)
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" },
                        { "device_code", deviceCode }
                    }),
                };

                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await _httpService.SendGetRequest(request);
                if (response.IsFailure) return Result.Failure<string>("Access token not received");

                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Value);
                if (tokenResponse is null) return Result.Failure<string>("Error deserializing access token");

                return Result.Success(tokenResponse.AccessToken);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        private async Task<Result<DeviceCodeResponse>> GetInitialDeviceCodeAsync(string initialAuthUrl)
        {
            try
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
                var request = new HttpRequestMessage(HttpMethod.Post, initialAuthUrl)
                {
                    Content = new StringContent($"client_id={_clientId}", Encoding.UTF8, "application/x-www-form-urlencoded"),
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await _httpService.SendGetRequest(request);
                if (response.IsFailure) return Result.Failure<DeviceCodeResponse>("Device number not received");

                var deviceCodeResult = JsonConvert.DeserializeObject<DeviceCodeResponse>(response.Value);
                if (deviceCodeResult is null) return Result.Failure<DeviceCodeResponse>("Error deserializing device code");

                return Result.Success(deviceCodeResult);
            }
            catch (Exception ex)
            {
                return Result.Failure<DeviceCodeResponse>(ex.Message);
            }
        }
    }
}
