using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    public class OfferRepository(DataContextAlias dataContext) : IOfferRepository
    {
        private readonly DataContextAlias _dataContext = dataContext;

        public async Task<bool> OfferExistsAsync(string offerId) => await _dataContext.Offerts.AnyAsync(x => x.OfferId == offerId);

        public async Task<Result<string>> GetOfferIdAsync()
        {
            var random = new Random();
            //var randomNumber = random.Next(1, 3);
            var randomNumber = 1;

            try
            {
                var orderId = await _dataContext.Orders.Where(x => x.Id == randomNumber)
                                                       .Select(x => x.OrderId)
                                                       .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(orderId)) return Result.Failure<string>("OrderId not found");

                return Result.Success(orderId);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error occured while getting OrderId");
            }
        }

        public async Task<Offer> GetOfferByIdAsync(string offerId)
        {
            var offer = await _dataContext.Offerts.FirstAsync(x => x.OfferId == offerId);
            return offer;  
        }

        public async Task<Result> SaveOfferAsync(Offer offer)
        {
            try
            {
                await _dataContext.Offerts.AddAsync(offer);
                await _dataContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }   
    }
}
