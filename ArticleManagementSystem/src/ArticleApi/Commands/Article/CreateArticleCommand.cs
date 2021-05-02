using System.Collections.Generic;
using ArticleApi.Queries;
using Domain.Article;
using MediatR;

namespace ArticleApi.Commands.Article
{
    public class CreateArticleCommand : IRequest<ArticleDto>
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