using System.Threading.Tasks;
using Domain.Article.Dtos;

namespace Domain.Article
{
    public interface IArticleDomainService
    {
        Task<Article> AddAsync(CreateArticleDto dto);
    }
}