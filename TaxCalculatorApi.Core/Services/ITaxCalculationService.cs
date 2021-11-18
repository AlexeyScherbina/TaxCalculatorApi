using TaxCalculatorApi.Core.Dto;

namespace TaxCalculatorApi.Core.Services
{
    public interface ITaxCalculationService
    {
        CalculationResultDto Calculate(BaseCalculationDto dto);
    }
}
