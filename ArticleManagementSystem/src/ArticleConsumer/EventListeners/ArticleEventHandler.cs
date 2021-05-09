using System.Linq;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Article.Events;
using Domain.Integration;
using Microsoft.Extensions.Logging;

namespace ArticleConsumer.EventListeners
{
    public class ArticleEventHandler : IEventHandler<CreatedArticleEvent>
    {
        private readonly IIntegrationSettingsRepository _integrationRepository;
        private readonly ILogger<ArticleEventHandler> _logger;

        public ArticleEventHandler(IIntegrationSettingsRepository integrationRepository, ILogger<ArticleEventHandler> logger)
        {
            _integrationRepository = integrationRepository;
            _logger = logger;
        }

        public Task Handle(CreatedArticleEvent @event)
        {
            _logger.LogInformation($"ArticleEventHandler handled event : {JsonSerializer.Serialize(@event)}");
            return Task.CompletedTask;
        }
    }
}