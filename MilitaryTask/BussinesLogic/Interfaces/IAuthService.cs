using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> DownloadDataAsync(string url, string authTokenUrl, string initialAuthUrl);
        Task<Result<string>> GetAuthTokenAsync(string url, string initialAuthUrl, HttpClient httpClient);
    }
}
