using Domain.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.TypeConfigurations
{
    public class ArticleTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Code);

            builder.Property(x => x.Title).HasMaxLength(512);
            builder.Property(x => x.Content).HasColumnType("text");
        }
    }
}