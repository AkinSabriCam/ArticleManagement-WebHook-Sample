using System;

namespace Api.Model
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Title { get;  set; }
        public string Content { get;  set; }
        public ArticleCategory Category { get; set; }
        public string AuthorName { get;  set; }
        public string AuthorLastName { get;  set; }
    }

    public enum ArticleCategory
    {

        Science,
        History,
        Technology,
        Geography,
        Medicine
    }
}