using System;
using System.Collections.Generic;
using Common.Entity;
using Common.Event;

namespace Domain.Article.Events
{
    public class CreatedArticleEvent : IEvent
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ArticleCategory Category { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
        public List<string> IntegrationCodes { get; set; }

        public CreatedArticleEvent()
        {

        }

        public CreatedArticleEvent(Article article, List<string> integrationCodes)
        {
            Id = article.Id;
            Code = article.Code;
            Title = article.Title;
            Content = article.Content;
            Category = article.Category;
            AuthorName = article.AuthorName;
            AuthorLastName = article.AuthorLastName;
            IntegrationCodes = integrationCodes;
        }


    }
}