using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IHttpService
    {
        HttpClient CreateClient();
        Task<Result<string>> SendGetRequest(HttpRequestMessage request);
        Result<HttpRequestMessage> CreateGetRequestWithParams(string baseUrl, string paramName, string paramValue);
        Task<Result<string>> SendGetRequestWithBearerToken(HttpRequestMessage request, string bearerToken);
    }
}
