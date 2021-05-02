using System;
using Common.Repository;
using Domain.Article;

namespace Infrastructure.EntityFramework.Repositories
{
    public class ArticleRepository : Repository<Article, Guid>, IArticleRepository
    {
        public ArticleRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}