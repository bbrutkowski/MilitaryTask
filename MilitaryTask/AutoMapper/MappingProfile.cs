using AutoMapper;
using MilitaryTask.Model;
using MilitaryTask.Model.DTO;

namespace MilitaryTask.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BillEntryDto, Bill>()
            .ForMember(x => x.BillId, o => o.MapFrom(src => src.Id))
            .ForMember(x => x.OccurredAt, o => o.MapFrom(src => src.OccurredAt))
            .ForMember(x => x.BillType, o => o.MapFrom(src => src.Type))
            .ForMember(x => x.Amount, o => o.MapFrom(src => new Amount { Value = src.Value.Amount, Currency = src.Value.Currency }))
            .ForMember(x => x.TaxRate, o => o.MapFrom(src => new TaxRate { Percentage = src.Tax.Percentage }))
            .ForMember(x => x.AccountBalance, o => o.MapFrom(src => new AccountBalance { Value = src.Balance.Amount, Currency = src.Balance.Currency }))
            .ForMember(x => x.Offer, o => o.MapFrom(src => new Offer { TenderId = src.Offer.Id, Name = src.Offer.Name }))
            .ForMember(x => x.Id, o => o.Ignore()) 
            .ForMember(x => x.BillTypeId, o => o.Ignore())
            .ForMember(x => x.AmountId, o => o.Ignore())
            .ForMember(x => x.TaxRateId, o => o.Ignore())
            .ForMember(x => x.AccountBalanceId, o => o.Ignore())
            .ForMember(x => x.OfferId, o => o.Ignore());

            CreateMap<BillTypeDto, BillType>()
                .ForMember(x => x.BillTypeId, o => o.MapFrom(src => src.Id))
                .ForMember(x => x.Name, o => o.MapFrom(src => src.Name))
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Bill, o => o.Ignore());

            CreateMap<OfferDto, Offer>()
                .ForMember(x => x.TenderId, o => o.MapFrom(src => src.Id))
                .ForMember(x => x.Name, o => o.MapFrom(src => src.Name))
                .ForMember(x => x.Id, o => o.Ignore()) 
                .ForMember(x => x.Bills, o => o.Ignore());

            CreateMap<ValueDto, Amount>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Bill, o => o.Ignore());

            CreateMap<TaxRateDto, TaxRate>()
                .ForMember(x => x.Id, o => o.Ignore()) 
                .ForMember(x => x.Bill, o => o.Ignore());

            CreateMap<AccountBalanceDto, AccountBalance>()
                .ForMember(x => x.Value, o => o.MapFrom(src => src.Amount))
                .ForMember(x => x.Id, o => o.Ignore()) 
                .ForMember(x => x.Bill, o => o.Ignore());
        }
    }
}
