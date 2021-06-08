using System;
using System.Threading.Tasks;
using Infrastructure.Model;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Api.Configuration
{
    public static class DbContextInitializeService
    {
        public static async Task MigrateAsync(this IServiceProvider serviceProvider)
        {
            using(var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var dbContextInitializer = scope.ServiceProvider.GetRequiredService<IDbContextInitializer>();
                    await dbContextInitializer.MigrateAsync();
                    Log.Logger.Information($"Migrations applied successfully.");

                }
                catch (Exception ex)
                {
                    Log.Logger.Fatal($"Migrations can not be applied, The Error : {ex.Message}");
                }
            }
        }
    }
}