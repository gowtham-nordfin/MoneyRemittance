using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model.Request;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Country
{
    public class ExchangeRateAndFeesHandler : MediatingRequestHandler<ExchangeRateAndFeesRequest, ExchangeRateAndFeesResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;

        public ExchangeRateAndFeesHandler(ILogger<ExchangeRateAndFeesHandler> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public override async Task<ExchangeRateAndFeesResponse> Handle(ExchangeRateAndFeesRequest request, CancellationToken cancellationToken)
        {
            var exchangeRateRequest = new ExchangeRateRequest
            {
                From = request.From,
                To = request.To
            };

            var feeListRequest = new FeesListRequest
            {
                From = request.From,
                To = request.To
            };

            var exchangeRateResponse = await _countryService.GetExchangeRate(exchangeRateRequest);
            var feeListResponse = await _countryService.GetFeeList(feeListRequest);
            var finalFee = feeListResponse.FirstOrDefault(f => f.Amount == request.Amount).Fee;

            return await Task.FromResult(new ExchangeRateAndFeesResponse
            {
                DestinationCountry = exchangeRateResponse.DestinationCountry,
                SourceCountry = exchangeRateResponse.SourceCountry,
                Fee = finalFee,
                ExchangeRate = exchangeRateResponse.ExchangeRate,
                ExchangeRateToken = exchangeRateResponse.ExchangeRateToken
            });
        }
    }
}
