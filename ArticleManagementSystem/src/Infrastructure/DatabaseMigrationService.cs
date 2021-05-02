using System;
using System.Threading.Tasks;
using Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DatabaseMigrationService
    {
        
        public static async Task Migrate(this IServiceProvider serviceProvider)
        {
            using(var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var appDbInitializer = scope.ServiceProvider.GetRequiredService<IAppDbInitializer>();
                await appDbInitializer.MigrateAsync();
            }
        }
    }
}