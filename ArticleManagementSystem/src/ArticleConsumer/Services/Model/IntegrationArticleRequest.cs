using System;
using Domain.Article;

namespace ArticleConsumer.Services.Model
{
    public class IntegrationArticleRequest
    {
        public ArticleRequestModel ArticleModel { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class ArticleRequestModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ArticleCategory Category { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
    }
}