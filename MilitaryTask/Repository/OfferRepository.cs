using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.DataContext;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.Repository
{
    public class OfferRepository(MyDataContext dataContext) : IOfferRepository
    {
        private readonly MyDataContext _dataContext = dataContext;

        public async Task<bool> OfferExistsAsync(string offerId) => await _dataContext.Offerts.AnyAsync(x => x.OfferId == offerId);

        public async Task<Offer> GetOfferByIdAsync(string offerId) => await _dataContext.Offerts.FirstAsync(x => x.OfferId == offerId);

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
