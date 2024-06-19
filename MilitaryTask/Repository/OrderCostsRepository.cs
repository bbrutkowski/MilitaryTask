using CSharpFunctionalExtensions;
using MilitaryTask.DataContext.Interface;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.Repository
{
    internal class OrderCostsRepository : IOrderCostsRespository
    {
        private readonly IDataContext _dataContext;
        private const string EmptyDataError = "No data to save";
        private const string SavaDataError = "Error occured while saving data";

        public OrderCostsRepository(IDataContext dataContext) => _dataContext = dataContext;

        public async Task<Result> SaveOrderCostsAsync(Order order)
        {
            if (order is null) return Result.Failure(EmptyDataError);

            try
            {
                await _dataContext.OrderTable.AddAsync(order);
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
