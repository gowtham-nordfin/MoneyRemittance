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
    public class BankListHandler : MediatingRequestHandler<BankListRequest, BankListResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;
        private readonly IConfiguration _configuration;

        public BankListHandler(ILogger<BankListHandler> logger, ICountryService countryService, IConfiguration configuration)
        {
            _logger = logger;
            _countryService = countryService;
            _configuration = configuration;
        }

        public override async Task<BankListResponse> Handle(BankListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var bankListRequest = new ServiceIntegration.Model.Request.BankListRequest
                {
                    Country = request.Country,
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var bankListForCountry = await _countryService.GetBanksList(bankListRequest);

                if(bankListForCountry.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, bankListForCountry.Error, null, bankListForCountry.ResponseCode);
                }

                var bankListResponse = new BankListResponse
                {
                    BankList = new List<Model.Bank>()
                };

                foreach (var item in bankListForCountry.BankList)
                {
                    bankListResponse.BankList.Add(new Model.Bank
                    {
                        Code = item.Code,
                        Name = item.Name
                    });
                }

                return await Task.FromResult(bankListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to fetch Bank list failed " + request.CorrelationId);
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
