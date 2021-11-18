namespace TaxCalculatorApi.Core.Dto
{
    public class CalculationResultDto
    {
        public decimal GrossIncome { get; set; }

        public decimal CharitySpent { get; set; }

        public decimal IncomeTax { get; set; }

        public decimal SocialTax { get; set; }

        public decimal TotalTax => IncomeTax + SocialTax;

        public decimal NetIncome => GrossIncome - TotalTax;
    }
}
