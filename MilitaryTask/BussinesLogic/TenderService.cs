using CSharpFunctionalExtensions;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;

namespace MilitaryTask.BussinesLogic
{
    public class TenderService : ITenderService
    {
        private readonly ITenderRepository _tenderRepository;

        public TenderService(ITenderRepository tenderRepository) => _tenderRepository = tenderRepository;

        public async Task<Result> SaveTenderAsync(List<BillingEntry> billingEntries)
        {
            if (!billingEntries.Any()) Result.Failure("No tender to save");

            try
            {
                var firstOffer = billingEntries.First().Offer;
                var allSame = billingEntries.All(entry => entry.Offer.Id == firstOffer.Id);
                if (!allSame)
                {
                    var result = await SaveTendersAsyns(billingEntries.Select(x => x.Offer).ToList());
                    if (result.IsFailure) return Result.Failure(result.Error);
                }

                var tender = new Tender()
                {
                    TenderId = firstOffer.Id,
                    Name = firstOffer.Name
                };

                var saveResult = await _tenderRepository.SaveTenderAsync(tender);
                if (saveResult.IsFailure) return Result.Failure(saveResult.Error);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        private async Task<Result> SaveTendersAsyns(List<Offer> offers)
        {
            var unique = new List<Tender>();

            try
            {
                var uniqueOffers = offers.GroupBy(x => x.Id)
                .Select(y => y.First())
                .ToList();

                foreach (var uniqueOffer in uniqueOffers)
                {
                    var tender = new Tender()
                    {
                        TenderId = uniqueOffer.Id,
                        Name = uniqueOffer.Name
                    };

                    unique.Add(tender);
                }

                var savingResult = await _tenderRepository.SaveTendersAsync(unique);
                if (savingResult.IsFailure) return Result.Failure(savingResult.Error);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
