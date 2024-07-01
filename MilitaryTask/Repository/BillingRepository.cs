using CSharpFunctionalExtensions;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    internal class BillingRepository : IBillingRespository
    {
        private readonly DataContextAlias _dataContext;

        public BillingRepository(DataContextAlias dataContext) => _dataContext = dataContext;
         
        public async Task<Result> SaveBillsAsync(List<Bill> bills)
        {
            try
            {
               await _dataContext.Bills.AddRangeAsync(bills);
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
