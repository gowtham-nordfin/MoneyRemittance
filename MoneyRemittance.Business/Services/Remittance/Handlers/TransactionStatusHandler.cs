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
    public class TransactionStatusHandler : MediatingRequestHandler<TransactionStatusRequest, TransactionStatusResponse>
    {
        private readonly ILogger _logger;
        private ITransactionService _transactionService;
        private readonly IConfiguration _configuration;

        public TransactionStatusHandler(ILogger<TransactionStatusHandler> logger, ITransactionService transactionService, IConfiguration configuration)
        {
            _logger = logger;
            _transactionService = transactionService;
            _configuration = configuration;
        }

        public override async Task<TransactionStatusResponse> Handle(TransactionStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionStatusRequest = new ServiceIntegration.Model.Request.TransactionStatusRequest
                {
                    TransactionId = request.TransactionId,
                    AccessKey = _configuration["RemittanceProviderAccessKey"]
                };

                var response = await _transactionService.TransactionStatus(transactionStatusRequest);

                if(response.RequestFailed)
                {
                    throw new BusinessException(request.CorrelationId, response.Error, null, response.ResponseCode);
                }

                var transactionNameResponse = new TransactionStatusResponse
                {
                    TransactionId = response.TransactionId,
                    Status = response.Status
                };

                return await Task.FromResult(transactionNameResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to transaction status failed " + request.CorrelationId);
                throw new BusinessException(request.CorrelationId, ex?.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
