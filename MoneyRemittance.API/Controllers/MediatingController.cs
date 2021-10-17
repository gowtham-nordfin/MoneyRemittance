using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyRemittance.Business.Extensions;
using MoneyRemittance.Business.Shared;
using System.Threading.Tasks;

namespace MoneyRemittance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatingController : ControllerBase
    {
        protected IMediator Mediator { get; }

        protected MediatingController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected async Task<IActionResult> HandleRequestAsync<TRequest, TResponse>(TRequest request)
           where TRequest : RequestBase<TResponse>
           where TResponse : class
        {
            if (request == null) return BadRequest();

            request.CorrelationId = HttpContext.Request.Headers.GetCorrelationIdFromHeader();

            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
