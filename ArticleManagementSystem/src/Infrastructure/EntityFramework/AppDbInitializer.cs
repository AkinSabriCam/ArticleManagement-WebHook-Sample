using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework
{
    public class AppDbInitializer : IAppDbInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<AppDbInitializer> _logger;

        public AppDbInitializer(AppDbContext dbContext, ILogger<AppDbInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task MigrateAsync()
        {
            try
            {
                var dbCreator = _dbContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (!(await dbCreator.ExistsAsync()))
                    await dbCreator.CreateAsync();

                var lastAppliedMigration = (await _dbContext.Database.GetAppliedMigrationsAsync()).LastOrDefault();
                var lastPendingMigration = (await _dbContext.Database.GetPendingMigrationsAsync()).LastOrDefault();

                var migrator = _dbContext.Database.GetService<IMigrator>();
                var script = migrator.GenerateScript(lastAppliedMigration, lastPendingMigration);

                var connection = await _dbContext.GetDbConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = script;
                    await command.ExecuteNonQueryAsync();
                };
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Databa Initializer error : {ex.Message}");
            }

        }
    }
}