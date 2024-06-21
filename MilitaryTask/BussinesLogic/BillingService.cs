﻿using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using Newtonsoft.Json;

namespace MilitaryTask.BussinesLogic
{
    internal class BillingService : IBillingService
    {
        private readonly IAuthService _fileService;
        private readonly IBillingRespository _orderCostsRespository;

        private readonly string _dataUrl = "https://api.allegro.pl/billing/billing-entries";
        private readonly string _authTokenUrl = "https://allegro.pl/auth/oauth/token";
        private readonly string _initialAuthUrl = "https://allegro.pl/auth/oauth/device";

        public BillingService(IAuthService fileService, IBillingRespository orderCostsRespository)
        {
            _fileService = fileService;
            _orderCostsRespository = orderCostsRespository;
        }

        public async Task<Result<BillingEntriesList>> GetBillingListAsync()
        {
            var downloadResult = await _fileService.DownloadDataAsync(_dataUrl, _authTokenUrl, _initialAuthUrl);
            if (downloadResult.IsFailure) return Result.Failure<BillingEntriesList>(downloadResult.Error);

            var billingEntries = await ConvertDataToBillingEntriesListAsync(downloadResult.Value);

            return Result.Success(billingEntries.Value);
        }

        public async Task<Result> SaveBillingsAsync(BillingEntriesList billings)
        {
            if (billings is null) return Result.Failure("No billings to save");

            var saveResult = await _orderCostsRespository.SaveBillingsAsync(billings);
            if (saveResult.IsFailure) return Result.Failure("Error occured while saving data");

            return Result.Success();
        } 

        private async Task<Result<BillingEntriesList>> ConvertDataToBillingEntriesListAsync(string data)
        {
            try
            {
                var billingEntries = JsonConvert.DeserializeObject<BillingEntriesList>(data) ?? new BillingEntriesList();
                if (billingEntries is null) return Result.Failure<BillingEntriesList>("The billing list is empty");
              
                return Result.Success(billingEntries);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error occurred while deserializing data. Method: {nameof(ConvertDataToBillingEntriesListAsync)}");
                return Result.Failure<BillingEntriesList>(e.Message);
            }
        }
    }
}
