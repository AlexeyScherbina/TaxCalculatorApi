using TaxCalculatorApi.Core.Dto;

namespace TaxCalculatorApi.Core.Services
{
    public interface IPayerStorageService
    {
        void AddPayer(PayerDto dto);

        string GetPayer(string ssn);
    }
}
