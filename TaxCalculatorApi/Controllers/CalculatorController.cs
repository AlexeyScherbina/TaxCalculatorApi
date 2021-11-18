using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaxCalculatorApi.Core.Dto;
using TaxCalculatorApi.Core.Services;

namespace TaxCalculatorApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class CalculatorController : ControllerBase
    {
        private readonly ITaxProcessingService _taxProcessingService;

        public CalculatorController(ITaxProcessingService taxProcessingService)
        {
            _taxProcessingService = taxProcessingService;
        }

        [ProducesResponseType(typeof(CalculationResultDto), (int)HttpStatusCode.OK)]
        [HttpPost("Calculate")]
        public object Calculate([FromBody] CalculationRequestDto dto)
        {
            return _taxProcessingService.ProcessPayerTaxes(dto);
        }
    }
}
