using AutoMapper;
using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using MilitaryTask.Model.DTO;
using MilitaryTask.Repository.Interfaces;
using Newtonsoft.Json;

namespace MilitaryTask.BussinesLogic
{
    internal class BillingService : IBillingService
    {
        private readonly IHttpService _httpService; 
        private readonly IBillingRespository _billingRepository;
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;
        private readonly IBillTypeRepository _billTypeRepository;

        private readonly string _billingBaseUrl = "https://api.allegro.pl/billing/billing-entries";
        private readonly string _offerIdParamName = "offer.id";

        public BillingService(IHttpService httpService,
                              IBillingRespository billingRespository,
                              IMapper mapper,
                              IOfferRepository offerRepository,
                              IBillTypeRepository billTypeRepository)
        {
            _httpService = httpService;
            _billingRepository = billingRespository;
            _mapper = mapper;
            _offerRepository = offerRepository;
            _billTypeRepository = billTypeRepository;
        }

        public async Task<Result<List<Bill>>> GetBillsByOfferIdAsync(string offerId, string authToken)
        {
            try
            {
                var requestBuildResult = _httpService.CreateGetRequestWithParams(_billingBaseUrl, _offerIdParamName, offerId);
                if (requestBuildResult.IsFailure) return Result.Failure<List<Bill>>(requestBuildResult.Error);

                var result = await _httpService.SendGetRequestWithTokenAsync(requestBuildResult.Value, authToken);
                if (result.IsFailure) return Result.Failure<List<Bill>>(result.Error);

                var convertResult = ConvertBillingEntryToBills(result.Value);
                if (convertResult.IsFailure) return Result.Failure<List<Bill>>(convertResult.Error);

                return Result.Success(convertResult.Value);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<List<Bill>>(ex.Message);
            }
        }

        public async Task<Result> SaveBillsAsync(List<Bill> bills)
        {
            try
            {
                if (!bills.Any()) return Result.Failure("No bills to save");

                foreach (var bill in bills)
                {
                    await ProcessOfferAsync(bill);
                    await ProcessBillTypeAsync(bill);
                }

                var savingResult = await _billingRepository.SaveBillsAsync(bills);
                if (savingResult.IsFailure) return Result.Failure(savingResult.Error);

                return Result.Success(savingResult);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        private async Task ProcessBillTypeAsync(Bill bill)
        {
            if (await _billTypeRepository.BillTypeExistsAsync(bill.BillType.BillTypeId))
            {
                bill.BillTypeId = await _billTypeRepository.GetBillTypeByIdAsync(bill.BillType.BillTypeId);
            }
            else
            {
                await _billTypeRepository.SaveBillTypeAsync(bill.BillType);
                bill.BillTypeId = bill.BillType.Id;
            }            
        }

        private async Task ProcessOfferAsync(Bill bill)
        {
            if (await _offerRepository.OfferExistsAsync(bill.Offer.OfferId))
            {
                bill.OfferId = await _offerRepository.GetOfferByIdAsync(bill.Offer.OfferId);
            }
            else
            {
                await _offerRepository.SaveOfferAsync(bill.Offer);
                bill.OfferId = bill.Offer.Id;
            }
        }

        private Result<List<Bill>> ConvertBillingEntryToBills(string data)
        {
            try
            {
                var billingEntries = JsonConvert.DeserializeObject<BillingEntriesListDto>(data) ?? new BillingEntriesListDto();
                if (!billingEntries.BillingEntries.Any()) return Result.Failure<List<Bill>>("The billing list is empty");

                var bills = _mapper.Map<List<Bill>>(billingEntries.BillingEntries);
              
                return Result.Success(bills);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Bill>>(ex.Message);
            }
        }
    }
}
