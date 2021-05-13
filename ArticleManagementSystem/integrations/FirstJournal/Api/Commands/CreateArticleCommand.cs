using System.Collections.Generic;
using Api.Model;
using MediatR;

namespace Api.Commands
{
    public class CreateArticleCommand : IRequest
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ArticleCategory Category { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
    }
}