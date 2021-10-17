using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Country.Handlers
{
    public class CountriesListHandler : MediatingRequestHandler<CountryListRequest, CountryListResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;

        public CountriesListHandler(ILogger<CountriesListHandler> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public override async Task<CountryListResponse> Handle(CountryListRequest request, CancellationToken cancellationToken)
        {
            var countryListRequest = new ServiceIntegration.Model.Request.CountryListRequest();

            var countryList = await _countryService.GetCountryList(countryListRequest);

            var countryListResponse = new CountryListResponse
            {
                CountryList = new List<Model.Country>()
            };

            foreach (var item in countryList)
            {
                countryListResponse.CountryList.Add(new Model.Country
                {
                    Code = item.Code,
                    Name = item.Name
                });
            }

            return await Task.FromResult(countryListResponse);
        }
    }
}
