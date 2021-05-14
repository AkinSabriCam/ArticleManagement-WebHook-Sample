using System.Collections.Generic;
using System.Threading.Tasks;
using ArticleConsumer.Services.Model;

namespace ArticleConsumer.Services
{
    public interface IIntegrationHttpClient
    {
        Task PostToIntegartions(List<IntegrationArticleRequest> requests);
    }
}