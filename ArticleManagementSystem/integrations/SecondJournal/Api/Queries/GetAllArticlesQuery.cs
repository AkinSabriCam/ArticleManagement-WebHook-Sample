using System.Collections.Generic;
using MediatR;

namespace Api.Queries
{
    public class GetAllArticlesQuery : IRequest<List<ArticleDto>>
    {

    }
}