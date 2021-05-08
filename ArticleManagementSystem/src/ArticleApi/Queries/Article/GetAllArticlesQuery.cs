using System.Collections.Generic;
using MediatR;

namespace ArticleApi.Queries.Article
{
    public class GetAllArticlesQuery : IRequest<List<ArticleDto>>
    {

    }
}