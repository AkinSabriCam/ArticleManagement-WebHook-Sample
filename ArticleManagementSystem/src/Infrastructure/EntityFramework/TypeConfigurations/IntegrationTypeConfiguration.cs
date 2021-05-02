using Domain.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.TypeConfigurations
{
    public class IntegrationTypeConfiguration : IEntityTypeConfiguration<Integration>
    {
        public void Configure(EntityTypeBuilder<Integration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Code, x.Url, x.EndPoint });
        }
    }
}