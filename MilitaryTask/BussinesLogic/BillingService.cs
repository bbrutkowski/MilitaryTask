using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using Newtonsoft.Json;

namespace MilitaryTask.BussinesLogic
{
    internal class BillingService : IBillingService
    {
        private readonly IAuthService _fileService;
        private readonly IOrderCostsRespository _orderCostsRespository;

        private readonly string _dataUrl = "https://api.allegro.pl/billing/billing-entries";
        private readonly string _authTokenUrl = "https://allegro.pl/auth/oauth/token";
        private readonly string _initialAuthUrl = "https://allegro.pl/auth/oauth/device";

        public BillingService(IAuthService fileService, IOrderCostsRespository orderCostsRespository)
        {
            _fileService = fileService;
            _orderCostsRespository = orderCostsRespository;
        }

        public async Task<Result<IReadOnlyCollection<BillingEntriesResponse>>> GetBillingListAsync()
        {
            var downloadResult = await _fileService.DownloadDataAsync(_dataUrl, _authTokenUrl, _initialAuthUrl);
            if (downloadResult.IsFailure) return Result.Failure<IReadOnlyCollection<BillingEntriesResponse>>(downloadResult.Error);

            var billingEntries = await ConvertDataToBillingListAsync(downloadResult.Value);

            return Result.Success(billingEntries.Value);
        }

        public async Task<Result> SaveBillingsAsync(string data)
        {
            var costsList = new List<Order>();
            //var rawData = await ConvertDataToOrderCostsListAsync(data);

            var saveResult = await _orderCostsRespository.SaveOrderCostsAsync(costsList);
            if (saveResult.IsFailure) return Result.Failure("Error occured while saving data");

            return Result.Success();
        } 

        private async Task<Result<IReadOnlyCollection<BillingEntriesResponse>>> ConvertDataToBillingListAsync(string data)
        {
            try
            {
                var orderCosts = JsonConvert.DeserializeObject<IReadOnlyCollection<BillingEntriesResponse>>(data) ?? new List<BillingEntriesResponse>();
                if (!orderCosts.Any()) return Result.Failure<IReadOnlyCollection<BillingEntriesResponse>>("The billing list is empty");
              
                return Result.Success(orderCosts);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error occurred while deserializing the data. Method: {nameof(ConvertDataToBillingListAsync)}");
                return Result.Failure<IReadOnlyCollection<BillingEntriesResponse>>(e.Message);
            }
        }
    }
}
