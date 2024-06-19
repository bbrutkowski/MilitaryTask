using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IFileService
    {
        Task<Result<string>> DownloadDataAsync(string url, string authTokenUrl);
        Task<Result<string>> GetAuthTokenAsync(string url);
    }
}
