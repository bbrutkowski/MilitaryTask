using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IBillingService
    {
        Task<Result<string>> GetBillingDetailsByOrderIdAsync(string orderId, string authToken);
        Task<Result> SaveBillingsAsync(BillingEntriesList billings);
        Task<Result<BillingEntriesList>> DeserializeFilesToBillingEntryListAsync(string data);
    }
}
