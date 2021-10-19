using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Exceptions;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Country
{
    public class ExchangeRateAndFeesHandler : MediatingRequestHandler<ExchangeRateAndFeesRequest, ExchangeRateAndFeesResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;
        private readonly IConfiguration _configuration;

        public ExchangeRateAndFeesHandler(ILogger<ExchangeRateAndFeesHandler> logger, ICountryService countryService, IConfiguration configuration)
        {
            _logger = logger;
            _countryService = countryService;
            _configuration = configuration;
        }

        public override async Task<ExchangeRateAndFeesResponse> Handle(ExchangeRateAndFeesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var exchangeRateRequest = new ExchangeRateRequest
                {
                    From = request.From,
                    To = request.To,
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var feeListRequest = new FeesListRequest
                {
                    From = request.From,
                    To = request.To,
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var exchangeRateResponse = await _countryService.GetExchangeRate(exchangeRateRequest);
                var feeListResponse = await _countryService.GetFeeList(feeListRequest);

                if(exchangeRateResponse.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, exchangeRateResponse.Error, null, exchangeRateResponse.ResponseCode);
                }

                if(feeListResponse.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, feeListResponse.Error, null, feeListResponse.ResponseCode);
                }

                var finalFee = feeListResponse.FeesList.FirstOrDefault(f => f.Amount == request.Amount).Fee;

                return await Task.FromResult(new ExchangeRateAndFeesResponse
                {
                    DestinationCountry = exchangeRateResponse.DestinationCountry,
                    SourceCountry = exchangeRateResponse.SourceCountry,
                    Fee = finalFee,
                    ExchangeRate = exchangeRateResponse.ExchangeRate,
                    ExchangeRateToken = exchangeRateResponse.ExchangeRateToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to fetch exchange rate & fee failed");
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
