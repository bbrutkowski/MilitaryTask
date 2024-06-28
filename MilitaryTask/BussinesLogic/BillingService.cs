using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using Newtonsoft.Json;

namespace MilitaryTask.BussinesLogic
{
    internal class BillingService : IBillingService
    {
        private readonly IHttpService _httpService; 
        private readonly IBillingRespository _billingRepository;

        private readonly string _billingUrl = "https://api.allegro.pl/billing/billing-entries";
        private readonly string _offerIdParamName = "offer.id";

        public BillingService(IHttpService httpService, IBillingRespository billingRespository)
        {
            _httpService = httpService;
            _billingRepository = billingRespository;
        }

        public async Task<Result<string>> GetBillingDetailsByOfferIdAsync(string orderId, string authToken)
        {

            try
            {
                var requestBuildResult = _httpService.CreateGetRequestWithParams(_billingUrl, _offerIdParamName, orderId);
                if (requestBuildResult.IsFailure) return Result.Failure<string>(requestBuildResult.Error);

                var result = await _httpService.SendGetRequestWithBearerToken(requestBuildResult.Value, authToken);

                return Result.Success(result.Value);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public async Task<Result> SaveSortedBillsAsync(List<Bill> bills)
        {
            var savingResult = await _billingRepository.SaveSortedBillsAsync(bills);
            if (savingResult.IsFailure) return Result.Failure(savingResult.Error);

            return Result.Success(savingResult);
        } 

        public async Task<Result<BillingEntriesList>> DeserializeDataToBillingEntryListAsync(string data)
        {
            try
            {
                var billingEntries = JsonConvert.DeserializeObject<BillingEntriesList>(data) ?? new BillingEntriesList();
                if (!billingEntries.BillingEntries.Any()) return Result.Failure<BillingEntriesList>("The billing list is empty");
              
                return Result.Success(billingEntries);
            }
            catch (Exception)
            {
                throw new ApplicationException($"Error occurred while deserializing data");
            }
        }

        public Result<List<Bill>> ConvertEntriesToBills(List<BillingEntry> billingEntries)
        {
            try
            {
                var bills = new List<Bill>();

                foreach (var billingEntry in billingEntries)
                {
                    var bill = new Bill()
                    {
                        Id = billingEntry.Id,
                        OccurredAt = billingEntry.OccurredAt,
                        Tender = new Tender() { Id = billingEntry.Offer.Id, Name = billingEntry.Offer.Name },
                        BillType = new BillType() { Id = billingEntry.Type.Id, Name = billingEntry.Type.Name },
                        Amount = new Amount() { Value = billingEntry.Value.Amount, Currency = billingEntry.Value.Currency },
                        TaxRate = new TaxRate() { Percentage = billingEntry.Tax.Percentage },
                        AccountBalance = new AccountBalance() { Value = billingEntry.Balance.Amount, Currency = billingEntry.Balance.Currency },
                    };

                    bills.Add(bill);
                }

                return Result.Success(bills);
            }
            catch (Exception)
            {
                return Result.Failure<List<Bill>>("An error occurred while mapping the data");
            }
        }
    }
}
