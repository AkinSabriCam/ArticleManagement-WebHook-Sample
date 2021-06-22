using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ArticleConsumer.Configuration;

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
                    services.RegisterMyServices(configuration);
                    services.AddMyMasstransit(configuration);
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
