﻿using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IOfferRepository
    {
        Task<bool> OfferExistsAsync(string offerId);
        Task<Offer> GetOfferByIdAsync(string offerId);
        Task<Result> SaveOfferAsync(Offer offer);
    }
}
