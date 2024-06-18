using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;

namespace MilitaryTask.BussinesLogic
{
    internal class OrderCostsService : IOrderCostsService
    {
        private readonly IHttpService _httpService;
        private readonly string _orderCostsDataUrl = "https://developer.allegro.pl/documentation/#operation/getBillingEntries";

        public OrderCostsService(IHttpService fileRepository) => _httpService = fileRepository;

        public async Task<Result<byte[]>> GetOrderCostsAsync()
        {
            var byteFile = await _httpService.DownloadDataAsync(_orderCostsDataUrl);
            if (byteFile.IsFailure) return Result.Failure<byte[]>(byteFile.Error);

            return Result.Success(byteFile.Value);
        }
    }
}
