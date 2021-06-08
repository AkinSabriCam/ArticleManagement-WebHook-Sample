using System.Text.Json;
using System.Threading.Tasks;
using Domain.Article.Events;
using Microsoft.Extensions.Logging;
using ArticleConsumer.Services;

namespace ArticleConsumer.EventListeners
{
    public class ArticleEventHandler : IEventHandler<CreatedArticleEvent>
    {
        private readonly IArticleIntegrationService _integrationService;
        private readonly ILogger<ArticleEventHandler> _logger;

        public ArticleEventHandler(ILogger<ArticleEventHandler> logger, IArticleIntegrationService integrationService)
        {
            _logger = logger;
            _integrationService = integrationService;
        }

        public async Task Handle(CreatedArticleEvent @event)
        {
            _logger.LogInformation($"ArticleEventHandler handled event : {JsonSerializer.Serialize(@event)}");
            await _integrationService.SendToIntegrations(@event);
        }
    }
}