using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IOrderService 
    {
        Task<Result<List<string>>> GetOrderIdsAsync();
    }
}
