using System.Threading.Tasks;
using ArticleApi.Commands.Article;
using ArticleApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ArticleDto>> Create(CreateArticleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}