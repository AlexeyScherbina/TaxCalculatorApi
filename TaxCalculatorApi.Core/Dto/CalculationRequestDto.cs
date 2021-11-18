namespace TaxCalculatorApi.Core.Dto
{
    public class CalculationRequestDto : BaseCalculationDto
    {
        public string FullName { get; set; }

        public string Ssn { get; set; }
    }
}
