using System.Threading.Tasks;
using Domain.Article.Events;

namespace ArticleConsumer.Services
{
    public interface IArticelntegrationService
    {
        Task SendToIntegrations(CreatedArticleEvent articleEvent);
    }
}