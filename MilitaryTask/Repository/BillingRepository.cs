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
         
        public async Task<Result> SaveBillingsAsync(IReadOnlyCollection<BillingEntry> billings)
        {
            if (billings is null) return Result.Failure(EmptyDataError);

            try
            {
                //await _dataContext.BillingEntries.AddRangeAsync(billings);
                await _dataContext.SaveChangesAsync(cancellationToken: default);

                return Result.Success();
            }
            catch (Exception)
            {
                throw new ApplicationException($"An error occurred while writing data to the database. Method: {nameof(SaveBillingsAsync)}");
            }                 
        }
    }
}
