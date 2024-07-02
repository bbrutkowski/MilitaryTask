using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IHttpService
    {
        HttpClient CreateClient();
        Result<HttpRequestMessage> CreateGetRequestWithParams(string baseUrl, string paramName, string paramValue);
        Task<Result<string>> GetResponseContentAsync(HttpRequestMessage request, string? bearerToken);
    }
}
