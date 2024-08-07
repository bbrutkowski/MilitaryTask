﻿using CSharpFunctionalExtensions;

namespace MilitaryTask.BussinesLogic.Interfaces
{
    public interface IHttpService
    {
        Result<HttpRequestMessage> CreateGetRequestWithParams(string baseUrl, string paramName, string paramValue);
        Task<Result<string>> GetResponseContentAsync(HttpRequestMessage request, bool withAuthToken, string? bearerToken);
    }
}
