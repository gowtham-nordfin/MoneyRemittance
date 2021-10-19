using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Exceptions;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Beneficiary.Handlers
{
    public class BeneficiaryNameHandler : MediatingRequestHandler<BeneficiaryNameRequest, BeneficiaryNameResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private IBeneficiaryService _beneficiaryService;

        public BeneficiaryNameHandler(ILogger<BeneficiaryNameHandler> logger, IBeneficiaryService beneficiaryService, IConfiguration configuration)
        {
            _logger = logger;
            _beneficiaryService = beneficiaryService;
            _configuration = configuration;
        }

        public override async Task<BeneficiaryNameResponse> Handle(BeneficiaryNameRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var beneficiaryNameRequest = new ServiceIntegration.Model.Request.BeneficiaryNameRequest
                {
                    AccountNumber = request.AccountNumber,
                    BankCode = request.BankCode,
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var response = await _beneficiaryService.GetBeneficiaryName(beneficiaryNameRequest);

                if(response.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, response.Error, null, response.ResponseCode);
                }

                var beneficiaryNameResponse = new BeneficiaryNameResponse
                {
                    AccountName = response.AccountName,
                    BankCode = request.BankCode,
                    AccountNumber = request.AccountNumber
                };

                return await Task.FromResult(beneficiaryNameResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to fetch beneficiary name failed");
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
