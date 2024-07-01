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
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OccurredAt, opt => opt.MapFrom(src => src.OccurredAt))
            .ForMember(dest => dest.BillType, opt => opt.MapFrom(src => new BillType { BillTypeId = src.Type.Id, Name = src.Type.Name }))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => new Amount { Value = src.Value.Amount, Currency = src.Value.Currency }))
            .ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => new TaxRate { Percentage = src.Tax.Percentage }))
            .ForMember(dest => dest.AccountBalance, opt => opt.MapFrom(src => new AccountBalance { Value = src.Balance.Amount, Currency = src.Balance.Currency }))
            .ForMember(dest => dest.Offer, opt => opt.MapFrom(src => new Offer { OfferId = src.Offer.Id, Name = src.Offer.Name }))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BillTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.AmountId, opt => opt.Ignore())
            .ForMember(dest => dest.TaxRateId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountBalanceId, opt => opt.Ignore())
            .ForMember(dest => dest.OfferId, opt => opt.Ignore());
        }  
    }
}
