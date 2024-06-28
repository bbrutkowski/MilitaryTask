using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> GetAuthAsync();
        Task<Result<string>> GetAuthTokenAsync(string deviceCode);
    }
}
