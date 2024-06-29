using CSharpFunctionalExtensions;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    public class TenderRepository : ITenderRepository
    {
        private readonly DataContextAlias _dataContext;

        public TenderRepository(DataContextAlias dataContextAlias) => _dataContext = dataContextAlias;

        public async Task<Result> SaveTenderAsync(Tender tender)
        {
            try
            {
                await _dataContext.AddAsync(tender);
                await _dataContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> SaveTendersAsync(List<Tender> tender)
        {
            try
            {
                await _dataContext.AddRangeAsync(tender);
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
