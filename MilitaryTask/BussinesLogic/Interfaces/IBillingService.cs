using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IBillingService
    {
        Task<Result<IReadOnlyCollection<BillingEntriesResponse>>> GetBillingListAsync();
        Task<Result> SaveBillingsAsync(string data);
    }
}
