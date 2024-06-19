using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IOrderCostsService
    {
        Task<Result<string>> GetOrderCostsAsync();
        Task<Result> SaveOrderCostsAsync(string data);
    }
}
