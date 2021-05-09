using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Domain.Integration;
using Infrastructure.EntityFramework.Repositories;
using MassTransit;
using Infrastructure.StartupConfigurations;
using ArticleConsumer.EventListeners;
using Domain.Article.Events;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ArticleConsumer
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
               .ConfigureHostConfiguration((config) => { config.AddEnvironmentVariables(prefix: "ASPNETCORE_"); })
               .ConfigureAppConfiguration((hostContext, config) =>
               {
                   config.SetBasePath(Environment.CurrentDirectory);
                   config.AddJsonFile("appsettings.json", optional: false);
                   config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                   config.AddEnvironmentVariables();

                   if (args != null)
                       config.AddCommandLine(args);

                   config.Build();
               })
               .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
                    services.AddScoped<IIntegrationSettingsRepository, IntegrationSettingsRepository>();
                    services.AddScoped<IEventHandler<CreatedArticleEvent>, ArticleEventHandler>();

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
                })
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.MinimumLevel.Information();
                    loggerConfiguration.WriteTo.Console();
                });


            await hostBuilder.RunConsoleAsync();
        }
    }
}
