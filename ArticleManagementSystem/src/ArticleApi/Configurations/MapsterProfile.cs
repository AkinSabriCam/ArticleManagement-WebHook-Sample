using ArticleApi.Commands.Article;
using ArticleApi.Commands.Integration;
using ArticleApi.Queries;
using ArticleApi.Queries.Integration;
using Domain.Article;
using Domain.Article.Dtos;
using Domain.Integration;
using Domain.Integration.Dtos;
using Mapster;

namespace ArticleApi.Configurations
{
    public class MapsterProfile
    {
        public void Configure()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);


            TypeAdapterConfig<CreateArticleCommand, CreateArticleDto>
                .NewConfig();


            TypeAdapterConfig<Article, ArticleDto>
               .NewConfig();

            TypeAdapterConfig<CreateIntegrationCommand, CreateIntegrationDto>
                .NewConfig();


            TypeAdapterConfig<Integration, IntegrationDto>
               .NewConfig();
        }
    }
}