using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    public class OfferRepository(DataContextAlias dataContext) : IOfferRepository
    {
        private readonly DataContextAlias _dataContext = dataContext;

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
    }
}
