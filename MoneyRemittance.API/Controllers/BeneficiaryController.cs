using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Services.Beneficiary;
using System.Threading.Tasks;

namespace MoneyRemittance.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class BeneficiaryController : MediatingController
    {
        private readonly ILogger<BeneficiaryController> _logger;

        public BeneficiaryController(IMediator mediator, ILogger<BeneficiaryController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost("beneficiary/name")]
        public async Task<IActionResult> GetBeneficiaryName([FromBody] BeneficiaryNameRequest beneficiaryNameRequest)
        {
            return await HandleRequestAsync<BeneficiaryNameRequest, BeneficiaryNameResponse>(beneficiaryNameRequest);
        }
    }
}
