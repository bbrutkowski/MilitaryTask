using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IBillRespository
    {
        Task<Result> SaveBillsAsync(List<Bill> billings);
    }
}
