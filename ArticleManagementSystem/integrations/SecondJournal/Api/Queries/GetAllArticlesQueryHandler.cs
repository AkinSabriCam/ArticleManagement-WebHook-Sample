using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Queries
{
    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, List<ArticleDto>>
    {
        public readonly AppDbContext _dbContext;

        public GetAllArticlesQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ArticleDto>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var articleDtoList = new List<ArticleDto>();
            var articles = await _dbContext.Articles.AsNoTracking().ToListAsync();

            if (!articles.Any())
                return articleDtoList;

            articles.ForEach(article =>
            {
                articleDtoList.Add(new ArticleDto
                {
                    Id = article.Id,
                    Code = article.Code,
                    Title = article.Title,
                    Content = article.Content,
                    AuthorName = article.AuthorName,
                    AuthorLastName = article.AuthorLastName,
                    Category = article.Category
                });
            });

            return articleDtoList;
        }
    }
}