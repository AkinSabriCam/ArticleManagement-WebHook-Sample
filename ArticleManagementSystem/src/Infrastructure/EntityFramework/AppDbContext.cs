using System.Data.Common;
using System;
using System.Threading.Tasks;
using Infrastructure.EntityFramework.TypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleTypeConfiguration).Assembly);
            RenameTablesAndPropertiesAsSnackCase(modelBuilder);
        }


        public async Task<DbConnection> GetDbConnection()
        {
            var connection = this.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            return connection;
        }

        private static void RenameTablesAndPropertiesAsSnackCase(ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in entities)
            {
                RenameTableNameAsSnackCase(entity);
                RenameTablePropertiesAsSnackCase(entity);
            }
        }

        private static void RenameTableNameAsSnackCase(IMutableEntityType entity)
        {
            var tableName = entity.GetTableName();

            var newTableName = string.Empty;
            for (var i = 0; i < tableName.Length; i++)
            {
                if (Char.IsUpper(tableName[i]))
                {
                    if (i > 0)
                        newTableName += '_';

                    newTableName += Char.ToLower(tableName[i]);
                    continue;
                }

                newTableName += tableName[i];
            }

            newTableName += 's';
            entity.SetTableName(newTableName);
        }

        private static void RenameTablePropertiesAsSnackCase(IMutableEntityType entity)
        {

            foreach (var property in entity.GetProperties())
            {
                var storeObjectIdentifier = StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema());
                var propertyName = property.GetColumnName(storeObjectIdentifier);

                var newPropertyName = string.Empty;
                for (var i = 0; i < propertyName.Length; i++)
                {
                    if (Char.IsUpper(propertyName[i]))
                    {
                        if (i > 0)
                            newPropertyName += '_';

                        newPropertyName += Char.ToLower(propertyName[i]);
                        continue;
                    }

                    newPropertyName += propertyName[i];
                }

                property.SetColumnName(newPropertyName);

            }
        }
    }
}