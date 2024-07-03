using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.BussinesLogic
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) => _orderRepository = orderRepository;
        public async Task<Result<List<string>>> GetOrderIdsAsync()
        {
            try
            {
                var orderIds = await _orderRepository.GetOrderIdsAsync();
                return Result.Success(orderIds.Value);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure<List<string>>(ex.Message);
            }
        }
    }
}
