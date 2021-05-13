using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Model
{
    public class AppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Article>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Code);
                b.Property(x => x.Title).HasMaxLength(512);
                b.Property(x => x.Content).HasColumnType("text");
            });

            base.OnModelCreating(modelBuilder);
            RenamedAsSnackCase(modelBuilder.Model);
        }

        private static void RenamedAsSnackCase(IMutableModel model)
        {

            var entityTypes = model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                RenamedPropertiesAsSnackCase(entityType);
                var tableName = entityType.GetTableName();
                var newTableName = new StringBuilder();
                for (var i = 0; i < tableName.Length; i++)
                {
                    if (char.IsUpper(tableName[i]))
                    {

                        if (i > 0)
                            newTableName.Append('_');

                        newTableName.Append(char.ToLowerInvariant(tableName[i]));
                        continue;
                    }
                    newTableName.Append(tableName[i]);
                }

                entityType.SetTableName(newTableName.ToString());
            }
        }

        private static void RenamedPropertiesAsSnackCase(IMutableEntityType entity)
        {
            var entityProperties = entity.GetProperties();

            foreach (var property in entityProperties)
            {
                var storeObjectIdentifier = StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema());
                var propertyName = property.GetColumnName(storeObjectIdentifier);
                var newPropertyName = new StringBuilder();
                for (var i = 0; i < propertyName.Length; i++)
                {
                    if (char.IsUpper(propertyName[i]))
                    {

                        if (i > 0)
                            newPropertyName.Append('_');

                        newPropertyName.Append(char.ToLowerInvariant(propertyName[i]));
                        continue;
                    }
                    newPropertyName.Append(propertyName[i]);
                }

                property.SetColumnName(newPropertyName.ToString());
            }
        }
    }
}