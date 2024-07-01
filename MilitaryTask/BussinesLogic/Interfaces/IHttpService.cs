using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IHttpService
    {
        HttpClient CreateClient();
        Task<Result<string>> SendGetRequestAsync(HttpRequestMessage request);
        Result<HttpRequestMessage> CreateGetRequestWithParams(string baseUrl, string paramName, string paramValue);
        Task<Result<string>> SendGetRequestAsync(HttpRequestMessage request, string bearerToken);
    }
}
