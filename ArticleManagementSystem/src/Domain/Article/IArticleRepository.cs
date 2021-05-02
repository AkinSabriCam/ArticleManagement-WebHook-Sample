using System;
using Common.Repository;

namespace Domain.Article
{
    public interface IArticleRepository : IRepository<Article, Guid>
    {
         
    }
}