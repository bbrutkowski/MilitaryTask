using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IOfferService
    {
        Task<Result<string>> GetOfferIdAsync();
    }
}
