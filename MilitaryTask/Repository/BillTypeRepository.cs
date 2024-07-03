using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.DataContext;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.Repository
{
    public class BillTypeRepository : IBillTypeRepository
    {
        private readonly MyDataContext _dataContext;

        public BillTypeRepository(MyDataContext dataContext) => _dataContext = dataContext;

        public async Task<bool> BillTypeExistsAsync(string billTypeId) => await _dataContext.BillTypes.AnyAsync(x => x.BillTypeId == billTypeId);

        public async Task<BillType> GetBillTypeByIdAsync(string billTypeId) => await _dataContext.BillTypes.FirstAsync(x => x.BillTypeId == billTypeId);

        public async Task<Result> SaveBillTypeAsync(BillType billType)
        {
            try
            {
                await _dataContext.BillTypes.AddAsync(billType);
                await _dataContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

    }
}
