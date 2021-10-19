using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Exceptions;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Country.Handlers
{
    public class CountriesListHandler : MediatingRequestHandler<CountryListRequest, CountryListResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;
        private readonly IConfiguration _configuration;

        public CountriesListHandler(ILogger<CountriesListHandler> logger, ICountryService countryService, IConfiguration configuration)
        {
            _logger = logger;
            _countryService = countryService;
            _configuration = configuration;
        }

        public override async Task<CountryListResponse> Handle(CountryListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var countryListRequest = new ServiceIntegration.Model.Request.CountryListRequest
                {
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var countryList = await _countryService.GetCountryList(countryListRequest);

                if(countryList.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, countryList.Error, null, countryList.ResponseCode);
                }

                var countryListResponse = new CountryListResponse
                {
                    CountryList = new List<Model.Country>()
                };

                foreach (var item in countryList.CountryList)
                {
                    countryListResponse.CountryList.Add(new Model.Country
                    {
                        Code = item.Code,
                        Name = item.Name
                    });
                }

                return await Task.FromResult(countryListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to fetch country list failed");
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
