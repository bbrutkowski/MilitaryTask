using CSharpFunctionalExtensions;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    internal class BillingRepository : IBillingRespository
    {
        private readonly DataContextAlias _dataContext;
        private const string EmptyDataError = "No data to save";
        private const string SavaDataError = "Error occured while saving data";

        public BillingRepository(DataContextAlias dataContext) => _dataContext = dataContext;
         
        public async Task<Result> SaveSortedBillsAsync(List<Bill> bills)
        {
            if (!bills.Any()) return Result.Failure("No bills to save");

            var sortedBills = bills.OrderBy(x => x.BillType.Id).ToList();
                    
            try
            {
                await _dataContext.Bills.AddRangeAsync(sortedBills);
                await _dataContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure($"An error occurred while saving data to database." +
                    $" Method: {nameof(SaveSortedBillsAsync)}");
            }                 
        }
    }
}
