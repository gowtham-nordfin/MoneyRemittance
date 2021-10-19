using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyRemittance.API.AuthorizeAttributes;
using MoneyRemittance.Business.Services.Remittance;
using System.Threading.Tasks;

namespace MoneyRemittance.API.Controllers
{
    [ApiKeyAuthorize]
    [ApiController]
    [Route("api/v1/")]
    public class MoneyRemittanceController : MediatingController
    {
        private readonly ILogger<MoneyRemittanceController> _logger;

        public MoneyRemittanceController(IMediator mediator, ILogger<MoneyRemittanceController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost("remittance/transaction")]
        public async Task<IActionResult> SubmitRemittanceTransaction([FromBody] SubmitTransactionRequest submitTransactionRequest)
        {
            return await HandleRequestAsync<SubmitTransactionRequest, SubmitTransactionResponse>(submitTransactionRequest);
        }

        [HttpGet("remittance/transaction/status")]
        public async Task<IActionResult> GetTransactionStatus(string transactionId)
        {
            var transactionStatusRequest = new TransactionStatusRequest
            {
                TransactionId = transactionId
            };

            return await HandleRequestAsync<TransactionStatusRequest, TransactionStatusResponse>(transactionStatusRequest);
        }
    }
}
