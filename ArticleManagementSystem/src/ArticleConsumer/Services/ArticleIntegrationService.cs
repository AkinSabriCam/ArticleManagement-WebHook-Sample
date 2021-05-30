using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleConsumer.Services.Model;
using Domain.Article.Events;
using Domain.Integration;
using Microsoft.Extensions.Logging;

namespace ArticleConsumer.Services
{
    public class ArticleIntegrationService : IArticleIntegrationService
    {
        private readonly IIntegrationSettingsRepository _integrationRepository;
        private readonly IIntegrationHttpClient _integrationHttpClient;
        private readonly ILogger<ArticleIntegrationService> _logger;

        public ArticleIntegrationService(
            IIntegrationSettingsRepository integrationRepository,
            IIntegrationHttpClient integrationHttpClient,
            ILogger<ArticleIntegrationService> logger)
        {
            _integrationRepository = integrationRepository;
            _integrationHttpClient = integrationHttpClient;
            _logger = logger;
        }

        public async Task SendToIntegrations(CreatedArticleEvent articleEvent)
        {
            var integrationArticleRequests = new List<IntegrationArticleRequest>();
            var articleRequestModel = CreateArticleRequestModel(articleEvent);

            foreach (var integrationCode in articleEvent.IntegrationCodes)
            {
                var IntegrationSetting = await _integrationRepository.GetByCodeAsync(integrationCode);

                if (IntegrationSetting == null)
                {
                    _logger.LogError($"There is no integration setting data by {integrationCode} integration code");
                    // In this case, we can send message to a queue to manage missing integration settings
                }

                integrationArticleRequests.Add(new IntegrationArticleRequest
                {
                    ArticleModel = articleRequestModel,
                    UserName = IntegrationSetting.UserName,
                    Password = IntegrationSetting.Password,
                    ClientId = IntegrationSetting.ClientId,
                    ClientSecret = IntegrationSetting.ClientSecret,
                    Url = IntegrationSetting.Url,
                    EndPoint = IntegrationSetting.EndPoint,
                    TokenUrl = IntegrationSetting.TokenUrl,
                    TokenEndPoint = IntegrationSetting.TokenEndPoint,
                    Code = integrationCode
                });

                _logger.LogInformation($"Integration settings successfully retrieved from database by {integrationCode} integration code");
            }

            if (integrationArticleRequests.Any())
                await _integrationHttpClient.PostToIntegartions(integrationArticleRequests);
            else
                _logger.LogInformation($"There is no any integration article requests!");

        }

        private static ArticleRequestModel CreateArticleRequestModel(CreatedArticleEvent articleEvent)
        {
            return new ArticleRequestModel
            {
                Id = articleEvent.Id,
                Code = articleEvent.Code,
                Title = articleEvent.Title,
                Content = articleEvent.Content,
                AuthorName = articleEvent.AuthorName,
                AuthorLastName = articleEvent.AuthorLastName,
                Category = articleEvent.Category,
            };
        }
    }
}