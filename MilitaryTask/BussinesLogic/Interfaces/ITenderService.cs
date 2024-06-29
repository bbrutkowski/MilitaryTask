using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface ITenderService
    {
        Task<Result> SaveTenderAsync(List<BillingEntry> billingEntries);
    }
}
