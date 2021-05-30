using System.Threading.Tasks;
using Domain.Article.Events;

namespace ArticleConsumer.Services
{
    public interface IArticleIntegrationService
    {
        Task SendToIntegrations(CreatedArticleEvent articleEvent);
    }
}