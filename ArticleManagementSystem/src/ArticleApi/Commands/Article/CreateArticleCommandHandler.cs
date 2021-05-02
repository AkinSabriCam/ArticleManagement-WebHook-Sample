using System.Threading;
using System.Threading.Tasks;
using ArticleApi.Queries;
using MediatR;

namespace ArticleApi.Commands.Article
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDto>
    {
        public Task<ArticleDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}