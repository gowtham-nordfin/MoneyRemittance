using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Services.Remittance.Request;
using RemittanceProvider.API.Model.Response;
using System.Threading.Tasks;

namespace MoneyRemittance.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoneyRemittanceController : MediatingController
    {
        private readonly ILogger<MoneyRemittanceController> _logger;

        public MoneyRemittanceController(IMediator mediator,ILogger<MoneyRemittanceController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost("ExchangeRateAndFees")]
        public async Task<IActionResult> GetExchangeRateAndFees([FromBody] ExchangeRateAndFeesRequest rateAndFeesRequest)
        {
            return await HandleRequestAsync<ExchangeRateAndFeesRequest, ExchangeRateAndFeesResponse>(rateAndFeesRequest);
        }
    }
}
