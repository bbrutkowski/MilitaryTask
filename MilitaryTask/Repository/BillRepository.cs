using CSharpFunctionalExtensions;
using MilitaryTask.DataContext;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.Repository
{
    internal class BillRepository : IBillRespository
    {
        private readonly MyDataContext _dataContext;

        public BillRepository(MyDataContext dataContext) => _dataContext = dataContext;

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
