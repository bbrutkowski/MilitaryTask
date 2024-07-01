using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    public class BillTypeRepository : IBillTypeRepository
    {
        private readonly DataContextAlias _dataContext;

        public BillTypeRepository(DataContextAlias dataContext) => _dataContext = dataContext;

        public async Task<bool> BillTypeExistsAsync(string billTypeId) => await _dataContext.BillTypes.AnyAsync(x => x.BillTypeId == billTypeId);
        public async Task<int> GetBillTypeByIdAsync(string billTypeId)
        {
            return await _dataContext.BillTypes.Where(x => x.BillTypeId == billTypeId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
        }

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
