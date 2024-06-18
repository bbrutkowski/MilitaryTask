using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;

namespace MilitaryTask.BussinesLogic
{
    internal class MyHttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyHttpService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<Result<byte[]>> DownloadDataAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var fileBytes = await httpClient.GetByteArrayAsync(url);

                return Result.Success(fileBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
                return Result.Failure<byte[]>(e.Message);
            }
        }
    }
}
