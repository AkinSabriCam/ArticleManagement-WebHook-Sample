using System;
using Domain.Article;

namespace ArticleApi.Queries
{
    public class ArticleDto
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