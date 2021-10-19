using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Exceptions;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Remittance
{
    public class SubmitTransactionHandler : MediatingRequestHandler<SubmitTransactionRequest, SubmitTransactionResponse>
    {
        private readonly ILogger _logger;
        private ITransactionService _transactionService;
        private readonly IConfiguration _configuration;

        public SubmitTransactionHandler(ILogger<SubmitTransactionHandler> logger, ITransactionService transactionService, IConfiguration configuration)
        {
            _logger = logger;
            _transactionService = transactionService;
            _configuration = configuration;
        }

        public override async Task<SubmitTransactionResponse> Handle(SubmitTransactionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var submitTransactionRequest = MapServiceToIntegration(request);
                submitTransactionRequest.AccessKey = _configuration["RemittanceProviderAccessKey"];

                var response = await _transactionService.SubmitTransaction(submitTransactionRequest);

                if(response.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, response.Error, null, response.ResponseCode);
                }

                var transactionNameResponse = new SubmitTransactionResponse
                {
                    TransactionId = response.TransactionId
                };

                return await Task.FromResult(transactionNameResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to submit the remittance transaction failed " + request.CorrelationId);
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private static ServiceIntegration.Model.Request.SubmitTransactionRequest MapServiceToIntegration(SubmitTransactionRequest inputRequest)
        {
            return new ServiceIntegration.Model.Request.SubmitTransactionRequest
            {
                DateOfBirth = inputRequest.DateOfBirth,
                ExchangeRate = inputRequest.ExchangeRate,
                Fees = inputRequest.Fees,
                FromAmount = inputRequest.FromAmount,
                FromCurrency = inputRequest.FromCurrency,
                SenderAddress = inputRequest.SenderAddress,
                SenderCity = inputRequest.SenderCity,
                SenderCountry = inputRequest.SenderCountry,
                SenderEmail = inputRequest.SenderEmail,
                SenderFirstName = inputRequest.SenderFirstName,
                SenderLastName = inputRequest.SenderLastName,
                SenderPhone = inputRequest.SenderPhone,
                SenderPostalCode = inputRequest.SenderPostalCode,
                SendFromState = inputRequest.SendFromState,
                ToBankAccountName = inputRequest.ToBankAccountName,
                ToBankAccountNumber = inputRequest.ToBankAccountNumber,
                ToBankCode = inputRequest.ToBankCode,
                ToBankName = inputRequest.ToBankName,
                ToCountry = inputRequest.ToCountry,
                ToFirstName = inputRequest.ToFirstName,
                ToLastName = inputRequest.ToLastName,
                TransactionNumber = inputRequest.TransactionNumber
            };
        }
    }
}
