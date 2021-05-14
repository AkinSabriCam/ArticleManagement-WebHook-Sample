using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ArticleConsumer.Services.Model;

namespace ArticleConsumer.Services
{
    public class IntegrationHttpClient : IIntegrationHttpClient
    {
        private readonly HttpClient _httpClient;

        public IntegrationHttpClient(HttpClient client)
        {
            _httpClient = client;
        }

        public Task PostToIntegartions(List<IntegrationArticleRequest> requests)
        {
            foreach (var request in requests)
            {
                // check cashe with username key , if there is no token send request to get token
                 



            }
            
    
            return Task.CompletedTask;
        }
/*
        private async Task<HttpResponseMessage> SendTokenRequest()
        {


        }

        private async Task<HttpResponseMessage> SendArticleRequest()
        {
             

        }*/
    }
}