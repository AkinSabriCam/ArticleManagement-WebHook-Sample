using System.Collections.Generic;
using System.Threading.Tasks;
using ArticleApi.Commands.Integration;
using ArticleApi.Queries.Integration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IntegrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<IntegrationDto>> Create(CreateIntegrationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<List<IntegrationDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllIntegrationsQuery());
        }
    }
}