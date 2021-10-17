using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Country.Handlers
{
    public class StateListHandler : MediatingRequestHandler<StateListRequest, StateListResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;

        public StateListHandler(ILogger<ExchangeRateAndFeesHandler> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public override async Task<StateListResponse> Handle(StateListRequest request, CancellationToken cancellationToken)
        {
            var stateListRequest = new ServiceIntegration.Model.Request.StateListRequest();

            var stateListForCountry = await _countryService.GetStateList(stateListRequest);

            var stateListResponse = new StateListResponse
            {
                StateList = new List<Model.State>()
            };

            foreach (var item in stateListForCountry)
            {
                stateListResponse.StateList.Add(new Model.State
                {
                    Code = item.Code,
                    Name = item.Name
                });
            }

            return await Task.FromResult(stateListResponse);
        }
    }
}
