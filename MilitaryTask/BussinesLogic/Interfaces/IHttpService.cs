using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IHttpService
    {
        Task<Result<byte[]>> DownloadDataAsync(string url);
    }
}
