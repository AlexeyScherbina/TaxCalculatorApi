using AutoMapper;
using TaxCalculatorApi.Core.Dto;
using TaxCalculatorApi.Core.Services;

namespace TaxCalculatorApi.Services
{
    public class ImaginariaTaxCalculationService : ITaxCalculationService
    {
        private const decimal MinTaxationAmount = 1000;
        private const decimal IncomeTax = 0.1M;
        private const decimal SocialContributions = 0.15M;
        private const decimal SocialContributionsMaxAmount = 3000;
        private const decimal MaxCharitySpent = 0.1M;

        private readonly IMapper _mapper;

        public ImaginariaTaxCalculationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CalculationResultDto Calculate(BaseCalculationDto dto)
        {
            var result = _mapper.Map<CalculationResultDto>(dto);
            var gross = dto.GrossIncome;

            if (dto.CharitySpent.HasValue)
            {
                var allowedCharity = gross * MaxCharitySpent;
                var charityDiscount = allowedCharity < dto.CharitySpent.Value ? allowedCharity : dto.CharitySpent.Value;
                gross -= charityDiscount;
            }

            if (gross <= MinTaxationAmount)
            {
                return result;
            }

            result.IncomeTax = (gross - MinTaxationAmount) * IncomeTax;

            var socialContributionsGross = (gross > SocialContributionsMaxAmount
                ? SocialContributionsMaxAmount
                : gross) - MinTaxationAmount;

            result.IncomeTax = gross * IncomeTax;
            result.SocialTax = socialContributionsGross * SocialContributions;

            return result;
        }
    }
}
