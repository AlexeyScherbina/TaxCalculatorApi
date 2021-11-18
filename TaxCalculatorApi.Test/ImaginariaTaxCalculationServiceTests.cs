using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using TaxCalculatorApi.Core.Dto;
using TaxCalculatorApi.Core.Services;
using TaxCalculatorApi.Services;
using TaxCalculatorApi.Test.Base;
using Xunit;

namespace TaxCalculatorApi.Test
{
    public class ImaginariaTaxCalculationServiceTests : BaseTest<ITaxCalculationService>
    {
        public ImaginariaTaxCalculationServiceTests()
        {
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            TestedInstance = new ImaginariaTaxCalculationService(mapper);
        }

        public static IEnumerable<object[]> CalculateTestData =>
            new List<object[]>
            {
                new object[] { new BaseCalculationDto { GrossIncome = 980 }, new CalculationResultDto { GrossIncome = 980 } },
                new object[] { new BaseCalculationDto { GrossIncome = 3400 }, new CalculationResultDto { GrossIncome = 3400, IncomeTax = 240, SocialTax = 300 } },
                new object[] { new BaseCalculationDto { GrossIncome = 2500, CharitySpent = 150 }, new CalculationResultDto { GrossIncome = 2500, IncomeTax = 135, SocialTax = 202.5M, CharitySpent = 150} },
                new object[] { new BaseCalculationDto { GrossIncome = 3600, CharitySpent = 520 }, new CalculationResultDto { GrossIncome = 3600, IncomeTax = 224, SocialTax = 300, CharitySpent = 520 } }
            };

        [Theory]
        [MemberData(nameof(CalculateTestData))]
        public void Calculate_ExampleData_CorrectOutput(BaseCalculationDto dto, CalculationResultDto expected)
        {
            var result = TestedInstance.Calculate(dto);

            result.Should().BeEquivalentTo(expected);
        }
    }
}