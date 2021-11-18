using Microsoft.Extensions.Caching.Memory;
using TaxCalculatorApi.Core.Dto;
using TaxCalculatorApi.Core.Services;

namespace TaxCalculatorApi.Services
{
    public class PayerStorageService : IPayerStorageService
    {
        private readonly IMemoryCache _cache;

        public PayerStorageService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddPayer(PayerDto dto)
        {
            _cache.Set(dto.Ssn, dto.FullName);
        }

        public string GetPayer(string ssn)
        {
            return _cache.TryGetValue(ssn, out string fullname) ? fullname : null;
        }
    }
}
