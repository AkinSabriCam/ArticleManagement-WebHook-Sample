using System.Threading;
using System.Threading.Tasks;
using ArticleApi.Queries;
using Common.UnitOfWork;
using Domain.Article;
using Domain.Article.Dtos;
using MediatR;

namespace ArticleApi.Commands.Article
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDto>
    {
        private readonly IArticleDomainService _domainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateArticleCommandHandler(IArticleDomainService domainService, IUnitOfWork unitOfWork)
        {
            _domainService = domainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var result = await _domainService.AddAsync(new CreateArticleDto
            {
                Code = request.Code,
                Title = request.Title,
                Content = request.Content,
                IntegrationCodes = request.IntegrationCodes,
                Category = request.Category,
                AuthorName = request.AuthorName,
                AuthorLastName = request.AuthorLastName
            });

            await _unitOfWork.SaveChangesAsync();

            return new ArticleDto
            {
                Id = result.Id,
                Code = result.Code,
                Title = result.Title,
                Content = result.Content,
                Category = result.Category,
                AuthorName = result.AuthorName,
                AuthorLastName = result.AuthorLastName
            };
        }
    }
}