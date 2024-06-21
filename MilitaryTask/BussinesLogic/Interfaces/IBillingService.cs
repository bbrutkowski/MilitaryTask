using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IBillingService
    {
        Task<Result<BillingEntriesList>> GetBillingListAsync();
        Task<Result> SaveBillingsAsync(BillingEntriesList billings);
    }
}
