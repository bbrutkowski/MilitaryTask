using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IBillingRespository
    {
        Task<Result> SaveBillsAsync(List<Bill> billings);
    }
}
