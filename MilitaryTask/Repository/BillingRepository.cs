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
         
        public async Task<Result> SaveBillingsAsync(BillingEntriesList billings)
        {
            if (billings is null) return Result.Failure(EmptyDataError);

            try
            {
                await _dataContext.Billings.AddRangeAsync(billings.BillingEntries);
                await _dataContext.SaveChangesAsync(cancellationToken: default);

                return Result.Success();
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(SavaDataError);
                return Result.Failure(e.Message);
            }                 
        }
    }
}
