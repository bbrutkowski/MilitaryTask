﻿using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IOfferRepository
    {
        Task<Result<string>> GetOfferIdAsync();
        Task<bool> OfferExistsAsync(string offerId);
        Task<int> GetOfferByIdAsync(string offerId);
        Task<Result> SaveOfferAsync(Offer offer);
    }
}
