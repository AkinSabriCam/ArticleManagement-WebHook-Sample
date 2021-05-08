using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Article;
using MediatR;

namespace ArticleApi.Queries.Article
{
    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, List<ArticleDto>>
    {
        private readonly IArticleRepository _repository;

        public GetAllArticlesQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ArticleDto>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllAsync<ArticleDto>();
        }
    }
}