using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ArticleConsumer.Infrastructure.Caching;
using ArticleConsumer.Services.Model;
using GreenPipes.Internals.Extensions;
using Microsoft.Extensions.Logging;
using Polly;

namespace ArticleConsumer.Services
{
    public class IntegrationHttpClient : IIntegrationHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IntegrationHttpClient> _logger;
        private readonly IRedisManager _redisManager;

        public IntegrationHttpClient(IHttpClientFactory clientFactory, ILogger<IntegrationHttpClient> logger, IRedisManager redisManager)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _redisManager = redisManager;
        }

        public async Task PostToIntegartions(List<IntegrationArticleRequest> requests)
        {
            foreach (var request in requests)
                await SendArticleRequest(request);
        }

        private async Task SendArticleRequest(IntegrationArticleRequest request)
        {
            var tokenModel = await _redisManager.GetOrRunAsync(request.Code, async () => await SendTokenRequest(request));
            var postModelContent = new StringContent(JsonSerializer.Serialize(request.ArticleModel), Encoding.UTF8, "application/json");

            var client = CreateClient(request.Url, tokenModel.Token);
            var response = await client.PostAsync($"/{request.EndPoint}", postModelContent);

            if (response.IsSuccessStatusCode)
                _logger.LogInformation($"Article request was sent successfully - {JsonSerializer.Serialize(request.ArticleModel)}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                await GetTokenAndSendRequestAgain(request, client, postModelContent);

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var policy = Policy
                        .Handle<Exception>()
                        .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(5));

                    await policy.ExecuteAsync(async () =>
                    {
                        HttpResponseMessage response = await client.PostAsync(request.EndPoint, postModelContent);

                        if (!response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                            throw new Exception(
                                $"Polly Retry - StatusCode: {response.StatusCode}, " +
                                $"Polly Retry - Request: {JsonSerializer.Serialize(request.ArticleModel)}, Polly Retry - Response: {responseString}");
                        }
                    })
                    .OrTimeout(TimeSpan.FromMinutes(5));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private async Task GetTokenAndSendRequestAgain(IntegrationArticleRequest request, HttpClient client, StringContent postModelContent)
        {
            var tokenModel = await SendTokenRequest(request);
            await _redisManager.SetAsync(request.Code, tokenModel);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenModel.Token);
            var response = await client.PostAsync($"/{request.EndPoint}", postModelContent);

            if (response.IsSuccessStatusCode)
                _logger.LogInformation($"Article request was sent successfully - {JsonSerializer.Serialize(request.ArticleModel)}");
            else
                _logger.LogError($"Article could not sent successfully! Http Status Code : {response.StatusCode}");

            await Task.CompletedTask;
        }

        private async Task<TokenResponseModel> SendTokenRequest(IntegrationArticleRequest request)
        {
            var tokenModel = new
            {
                Username = request.UserName,
                Password = request.Password,
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
            };

            var postModelContent = new StringContent(JsonSerializer.Serialize(tokenModel), Encoding.UTF8,
                 "application/json");
            var client = CreateClient(request.TokenUrl, null);
            var response = await client.PostAsync($"/{request.TokenEndPoint}", postModelContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TokenResponseModel>(result);
            }

            try
            {
                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(5));

                return await policy.ExecuteAsync(async () =>
                    {
                        HttpResponseMessage response = await client.PostAsync(request.TokenEndPoint, postModelContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            return JsonSerializer.Deserialize<TokenResponseModel>(result);
                        }

                        var responseString = await response.Content.ReadAsStringAsync();
                        throw new Exception(
                            $"Polly Retry - Token Request StatusCode: {response.StatusCode}, " +
                            $"Polly Retry - Token Request: {JsonSerializer.Serialize(tokenModel)}, Polly Retry - Token Response: {responseString}");

                    })
                    .OrTimeout(TimeSpan.FromMinutes(5));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private HttpClient CreateClient(string url, string token)
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = new Uri(url);

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}