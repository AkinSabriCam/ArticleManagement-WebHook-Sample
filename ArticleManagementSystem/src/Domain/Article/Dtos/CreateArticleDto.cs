using System.Collections.Generic;

namespace Domain.Article.Dtos
{
    public class CreateArticleDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> IntegrationCodes { get; set; }
        public ArticleCategory Category { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
    }
}