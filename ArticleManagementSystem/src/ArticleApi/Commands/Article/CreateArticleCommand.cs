using System.Collections.Generic;
using ArticleApi.Queries;
using Domain.Article;
using FluentValidation;
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

    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty()
            .WithMessage("Article code can not be null or empty!").WithErrorCode("AMS101");

            RuleFor(x => x.Title).NotNull().NotEmpty()
            .WithMessage("Article title can not be null or empty!").WithErrorCode("AMS102");

            RuleFor(x => x.Content).NotNull().NotEmpty()
            .WithMessage("Article content can not be null or empty!").WithErrorCode("AMS103");

            RuleFor(x => x.IntegrationCodes).NotNull()
            .WithMessage("Integration code list not be null!").WithErrorCode("AMS104");

            RuleFor(x => x.AuthorName).NotNull().NotEmpty()
            .WithMessage("Article author name can not be null or empty!").WithErrorCode("AMS105");

            RuleFor(x => x.AuthorLastName).NotNull().NotEmpty()
            .WithMessage("Article author lastname can not be null or empty!").WithErrorCode("AMS106");
        }
    }
}