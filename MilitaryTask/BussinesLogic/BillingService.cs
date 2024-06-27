using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using Newtonsoft.Json;

namespace MilitaryTask.BussinesLogic
{
    internal class BillingService : IBillingService
    {
        private readonly IHttpService _httpService;

        private readonly string _billingUrl = "https://api.allegro.pl/billing/billing-entries";
        private readonly string _orderIdParamName = "order.id";

        public BillingService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Result<string>> GetBillingDetailsByOrderIdAsync(string orderId, string authToken)
        {

            try
            {
                var requestBuildResult = _httpService.CreateGetRequestWithParams(_billingUrl, _orderIdParamName, orderId);
                if (requestBuildResult.IsFailure) return Result.Failure<string>(requestBuildResult.Error);

                var result = await _httpService.SendGetRequestWithBearerToken(requestBuildResult.Value, authToken);

                return Result.Success(result.Value);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public async Task<Result> SaveBillingsAsync(BillingEntriesList billings)
        {
            try
            {
                //if (billings is null) return Result.Failure("No billings to save");

                //var saveResult = await _orderCostsRespository.SaveBillingsAsync(billings.BillingEntries);
                //if (saveResult.IsFailure) return Result.Failure(saveResult.Error);

                return Result.Success();
            }
            catch (ApplicationException ex)
            {
                return Result.Failure(ex.Message);
            }
        } 

        public async Task<Result<BillingEntriesList>> DeserializeFilesToBillingEntryListAsync(string data)
        {
            try
            {
                var billingEntries = JsonConvert.DeserializeObject<BillingEntriesList>(data) ?? new BillingEntriesList();
                if (billingEntries is null) return Result.Failure<BillingEntriesList>("The billing list is empty");
              
                return Result.Success(billingEntries);
            }
            catch (Exception)
            {
                throw new ApplicationException($"Error occurred while deserializing data." +
                    $" Method: {nameof(DeserializeFilesToBillingEntryListAsync)}");
            }
        }
    }
}
