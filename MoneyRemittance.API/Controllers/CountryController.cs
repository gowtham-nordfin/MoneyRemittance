using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyRemittance.API.AuthorizeAttributes;
using MoneyRemittance.Business.Services.Country;
using System.Threading.Tasks;

namespace MoneyRemittance.API.Controllers
{
    [ApiKeyAuthorize]
    [Route("api/v1/")]
    [ApiController]
    public class CountryController : MediatingController
    {
        private readonly ILogger<CountryController> _logger;

        public CountryController(IMediator mediator, ILogger<CountryController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countryListRequest = new CountryListRequest();
            return await HandleRequestAsync<CountryListRequest, CountryListResponse>(countryListRequest);
        }

        [HttpGet("country/banks")]
        public async Task<IActionResult> GetBanksForCountry(string countryCode)
        {
            var bankListRequest = new BankListRequest
            {
                Country = countryCode
            };

            return await HandleRequestAsync<BankListRequest, BankListResponse>(bankListRequest);
        }

        [HttpGet("country/states")]
        public async Task<IActionResult> GetStatesForCountry(string countryCode)
        {
            var stateListRequest = new StateListRequest
            {
                Country = countryCode
            };

            return await HandleRequestAsync<StateListRequest, StateListResponse>(stateListRequest);
        }

        [HttpPost("exchangeratefees")]
        public async Task<IActionResult> GetExchangeRateAndFees([FromBody] ExchangeRateAndFeesRequest rateAndFeesRequest)
        {
            return await HandleRequestAsync<ExchangeRateAndFeesRequest, ExchangeRateAndFeesResponse>(rateAndFeesRequest);
        }
    }
}
