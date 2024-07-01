using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IBillingService
    {
        Task<Result<string>> GetBillsByOfferIdAsync(string orderId, string authToken);
        Task<Result> SaveBillsAsync(List<Bill> billings);
        Task<Result<BillingEntriesList>> DeserializeDataToBillingEntryListAsync(string data);
        Result<List<Bill>> ConvertEntriesToBills(List<BillingEntry> billingEntries);
    }
}
