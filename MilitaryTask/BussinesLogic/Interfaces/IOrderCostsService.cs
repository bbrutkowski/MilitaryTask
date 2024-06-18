using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IOrderCostsService
    {
        Task<Result<byte[]>> GetOrderCostsAsync();
    }
}
