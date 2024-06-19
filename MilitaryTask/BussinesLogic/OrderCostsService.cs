using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MilitaryTask.BussinesLogic
{
    internal class OrderCostsService : IOrderCostsService
    {
        private readonly IFileService _fileService;
        private readonly IOrderCostsRespository _orderCostsRespository;

        private readonly string _orderCostsDataUrl = "https://developer.allegro.pl/documentation/#operation/getBillingEntries";
        private readonly string _dataUrl = "https://api.allegro.pl/billing/billing-entries";
        private readonly string _authTokenUrl = "https://allegro.pl/auth/oauth/token";

        public OrderCostsService(IFileService fileService, IOrderCostsRespository orderCostsRespository)
        {
            _fileService = fileService;
            _orderCostsRespository = orderCostsRespository;
        }

        public async Task<Result<string>> GetOrderCostsAsync()
        {
            var byteFile = await _fileService.DownloadDataAsync(_dataUrl, _authTokenUrl);
            if (byteFile.IsFailure) return Result.Failure<string>(byteFile.Error);

            return Result.Success(byteFile.Value);
        }

        public async Task<Result> SaveOrderCostsAsync(string data)
        {
            //var rawData = await ConvertDataToOrderCostsListAsync(data);


            return Result.Success();
        } 

        private async Task<Result<List<Order>>> ConvertDataToOrderCostsListAsync(byte[] data)
        {
            var orderCosts = new List<Order>();

            try
            {
                var dataSource = Encoding.UTF8.GetString(data);
                orderCosts = (List<Order>)(JsonConvert.DeserializeObject<IReadOnlyCollection<Order>>(dataSource) ?? new List<Order>());
                return Result.Success(orderCosts);
            }
            catch (Exception e)
            {
                throw;
            }
        }




    }
}
