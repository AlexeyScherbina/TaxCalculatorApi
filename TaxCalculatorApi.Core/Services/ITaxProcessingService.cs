using TaxCalculatorApi.Core.Dto;

namespace TaxCalculatorApi.Core.Services
{
    public interface ITaxProcessingService
    {
        CalculationResultDto ProcessPayerTaxes(CalculationRequestDto dto);
    }
}
