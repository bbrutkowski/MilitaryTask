using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.BussinesLogic
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository) => _offerRepository = offerRepository;

        public async Task<Result<string>> GetOfferIdAsync()
        {
            try
            {
                var orderId = await _offerRepository.GetOfferIdAsync();
                return Result.Success(orderId.Value);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}
