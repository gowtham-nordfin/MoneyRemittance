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
    public class StateListHandler : MediatingRequestHandler<StateListRequest, StateListResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;
        private readonly IConfiguration _configuration;

        public StateListHandler(ILogger<StateListHandler> logger, ICountryService countryService, IConfiguration configuration)
        {
            _logger = logger;
            _countryService = countryService;
            _configuration = configuration;
        }

        public override async Task<StateListResponse> Handle(StateListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var stateListRequest = new ServiceIntegration.Model.Request.StateListRequest
                {
                    Country = request.Country,
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var stateListForCountry = await _countryService.GetStateList(stateListRequest);

                if(stateListForCountry.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, stateListForCountry.Error, null, stateListForCountry.ResponseCode);
                }

                var stateListResponse = new StateListResponse
                {
                    StateList = new List<Model.State>()
                };

                foreach (var item in stateListForCountry.StateList)
                {
                    stateListResponse.StateList.Add(new Model.State
                    {
                        Code = item.Code,
                        Name = item.Name
                    });
                }

                return await Task.FromResult(stateListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to fetch state list failed");
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
