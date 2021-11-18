using System.Text.RegularExpressions;
using AutoMapper;
using TaxCalculatorApi.Core.Dto;
using TaxCalculatorApi.Core.Exceptions;
using TaxCalculatorApi.Core.Services;

namespace TaxCalculatorApi.Services
{
    public class TaxProcessingService : ITaxProcessingService
    {
        private readonly ITaxCalculationService _taxCalculationService;
        private readonly IPayerStorageService _payerStorageService;
        private readonly IMapper _mapper;

        public TaxProcessingService(
            ITaxCalculationService taxCalculationService,
            IPayerStorageService payerStorageService,
            IMapper mapper)
        {
            _taxCalculationService = taxCalculationService;
            _payerStorageService = payerStorageService;
            _mapper = mapper;
        }

        public CalculationResultDto ProcessPayerTaxes(CalculationRequestDto dto)
        {
            ValidateCalculationRequestDto(dto);

            var payer = _mapper.Map<PayerDto>(dto);

            SavePayer(payer);

            return _taxCalculationService.Calculate(dto);
        }

        private void ValidateCalculationRequestDto(CalculationRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                throw new ValidationException("FullName is missing");
            }
            if (dto.FullName.Split(' ').Length < 2)
            {
                throw new ValidationException("FullName should contain at least two words separated by space");
            }
            if (Regex.IsMatch(dto.FullName, "[^0-9]"))
            {
                throw new ValidationException("FullName should contain symbols letters and spaces only");
            }

            if (string.IsNullOrWhiteSpace(dto.Ssn))
            {
                throw new ValidationException("SSN is missing");
            }
            if (Regex.IsMatch(dto.Ssn, "^[0-9]+$"))
            {
                throw new ValidationException("SSN should contain only digits");
            }
            if (dto.Ssn.Length is < 5 or > 10)
            {
                throw new ValidationException("SSN should contain from 5 to 10 digits");
            }

            if (dto.GrossIncome <= 0)
            {
                throw new ValidationException("GrossIncome is invalid");
            }

            if (dto.CharitySpent is <= 0)
            {
                throw new ValidationException("CharitySpent is invalid");
            }
        }

        private void SavePayer(PayerDto dto)
        {
            var existingPayer = _payerStorageService.GetPayer(dto.Ssn);

            if (existingPayer != null)
            {
                if (existingPayer != dto.Ssn)
                {
                    throw new ValidationException($"Payer with SSN '{dto.Ssn}' already exists.");
                }

                return;
            }

            _payerStorageService.AddPayer(dto);
        }
    }
}
