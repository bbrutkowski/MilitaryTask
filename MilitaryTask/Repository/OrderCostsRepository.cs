using CSharpFunctionalExtensions;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    internal class OrderCostsRepository : IOrderCostsRespository
    {
        private readonly DataContextAlias _dataContext;
        private const string EmptyDataError = "No data to save";
        private const string SavaDataError = "Error occured while saving data";

        public OrderCostsRepository(DataContextAlias dataContext) => _dataContext = dataContext;

        public async Task<Result> SaveOrderCostsAsync(List<Order> orders)
        {
            if (!orders.Any()) return Result.Failure(EmptyDataError);

            try
            {
                await _dataContext.OrderTable.AddRangeAsync(orders);
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
