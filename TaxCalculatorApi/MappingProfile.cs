using AutoMapper;
using TaxCalculatorApi.Core.Dto;

namespace TaxCalculatorApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CalculationRequestDto, PayerDto>();
            CreateMap<BaseCalculationDto, CalculationResultDto>()
                .ForMember(
                    dest => dest.NetIncome,
                    opt => opt.MapFrom(src =>
                        src.GrossIncome - (src.CharitySpent ?? default)));
        }
    }
}
