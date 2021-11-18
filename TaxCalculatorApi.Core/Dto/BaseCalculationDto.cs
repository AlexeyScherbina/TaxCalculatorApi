namespace TaxCalculatorApi.Core.Dto
{
    public class BaseCalculationDto
    {
        public decimal GrossIncome { get; set; }

        public decimal? CharitySpent { get; set; }
    }
}
