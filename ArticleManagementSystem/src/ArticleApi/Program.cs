using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace ArticleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
            .UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.MinimumLevel.Debug();
                loggerConfiguration.MinimumLevel.Information();
                loggerConfiguration.WriteTo.Console();
            })
            .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
