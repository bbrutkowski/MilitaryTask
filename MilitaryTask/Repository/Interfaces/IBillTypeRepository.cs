using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IBillTypeRepository
    {
        Task<bool> BillTypeExistsAsync(string billTypeId);
        Task<int> GetBillTypeByIdAsync(string billTypeId);
        Task<Result> SaveBillTypeAsync(BillType billType);
    }
}
