using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IBillingService
    {
        Task<Result<string>> GetBillingDetailsByOfferIdAsync(string orderId, string authToken);
        Task<Result> SaveSortedBillsAsync(List<Bill> billings);
        Task<Result<BillingEntriesList>> DeserializeDataToBillingEntryListAsync(string data);
        Result<List<Bill>> ConvertEntriesToBills(List<BillingEntry> billingEntries);
    }
}
