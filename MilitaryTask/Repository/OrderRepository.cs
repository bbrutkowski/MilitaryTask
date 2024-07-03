using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.DataContext;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDataContext _dataContext;

        public OrderRepository(MyDataContext dataContext) => _dataContext = dataContext;

        public async Task<Result<List<string>>> GetOrderIdsAsync()
        {
            try
            {
                var orderIds = await _dataContext.Orders.Select(x => x.OrderId).ToListAsync();

                if (!orderIds.Any()) return Result.Failure<List<string>>("No Id found");

                return Result.Success(orderIds);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error occured while getting OrderId");
            }
        }
    }
}
