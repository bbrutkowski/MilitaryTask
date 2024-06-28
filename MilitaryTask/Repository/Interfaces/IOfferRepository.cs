using CSharpFunctionalExtensions;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IOfferRepository
    {
        Task<Result<string>> GetOfferIdAsync();
    }
}
