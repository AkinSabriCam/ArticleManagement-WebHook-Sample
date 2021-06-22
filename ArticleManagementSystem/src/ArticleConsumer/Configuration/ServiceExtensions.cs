using System;
using ArticleConsumer.EventListeners;
using ArticleConsumer.Infrastructure.Caching;
using ArticleConsumer.Services;
using Domain.Article.Events;
using Domain.Integration;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.StartupConfigurations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleConsumer.Configuration
{
    public static class ServiceExtensions
    {
        public static void RegisterMyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddScoped<IIntegrationSettingsRepository, IntegrationSettingsRepository>();
            services.AddScoped<IEventHandler<CreatedArticleEvent>, ArticleEventHandler>();
            services.AddScoped<IArticleIntegrationService, ArticleIntegrationService>();
            services.AddScoped<IIntegrationHttpClient, IntegrationHttpClient>();
            services.AddScoped<IRedisCacheDbProvider, RedisCacheDbProvider>();
            services.AddScoped<IRedisManager, RedisManager>();

            services.Configure<RedisSettingsModel>(configuration.GetSection("RedisSettings"));
        }

        public static void AddMyMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitmqSettings();
            configuration.GetSection("RabbitMq").Bind(options);

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configurator =>
                {
                    configurator.Host(new Uri($"{options.Host}:{options.Port}"),
                    h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });

                    configurator.ReceiveEndpoint("article_events", receiverConfigurator =>
                    {
                        receiverConfigurator.Handler<CreatedArticleEvent>(async context =>
                        {
                            var articleEventHandler = services.BuildServiceProvider().GetRequiredService<IEventHandler<CreatedArticleEvent>>();
                            await articleEventHandler.Handle(context.Message);
                        });
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}