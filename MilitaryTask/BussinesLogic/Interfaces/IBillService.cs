using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IBillService
    {
        Task<Result<List<Bill>>> GetBillsByOfferIdAsync(List<string> offerIds, string authToken);
        Task<Result> SaveBillsAsync(List<Bill> billings);
    }
}
