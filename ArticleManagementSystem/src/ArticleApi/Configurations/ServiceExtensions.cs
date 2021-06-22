using System;
using ArticleApi.Commands.Article;
using Common.Event;
using Common.UnitOfWork;
using Domain.Article;
using Domain.Integration;
using FluentValidation;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.MassTransit;
using Infrastructure.StartupConfigurations;
using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleApi.Configurations
{
    public static class ServiceExtensions
    {
        public static void RegisterMyServices(this IServiceCollection services)
        {

            services.AddMediatR(typeof(CreateArticleCommand).Assembly);
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleDomainService, ArticleDomainService>();

            services.AddScoped<IIntegrationSettingsRepository, IntegrationSettingsRepository>();
            services.AddScoped<IIntegrationSettingsDomainService, IntegrationSettingsDomainService>();
            services.AddScoped<IAppDbInitializer, AppDbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPipelineBehavior<,>));
            services.AddValidatorsFromAssemblies(new[] { typeof(CreateArticleCommandValidator).Assembly });
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
        }

        public static void AddMyMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitmqSettings();
            configuration.GetSection("Rabbitmq").Bind(options);

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
                    }));
            });

            services.AddMassTransitHostedService();
        }
    }
}