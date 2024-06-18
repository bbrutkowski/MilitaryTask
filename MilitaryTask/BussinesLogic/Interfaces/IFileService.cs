using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IFileService
    {
        Task<Result<byte[]>> DownloadDataAsync(string url);
    }
}
