using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.BussinesLogic
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<Result<string>> GetOrderIdAsync()
        {
            try
            {
                var orderId = await _orderRepository.GetOrderIdAsync();
                return Result.Success(orderId.Value);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}
