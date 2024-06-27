using CSharpFunctionalExtensions;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<Result<string>> GetOrderIdAsync();
    }
}
