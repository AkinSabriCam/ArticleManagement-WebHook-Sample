using System.Threading;
using System.Threading.Tasks;
using ArticleApi.Queries;
using Common.UnitOfWork;
using Domain.Article;
using Domain.Article.Dtos;
using MapsterMapper;
using MediatR;

namespace ArticleApi.Commands.Article
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDto>
    {
        private readonly IArticleDomainService _domainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateArticleCommandHandler(IArticleDomainService domainService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _domainService = domainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ArticleDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var createArticleDto = _mapper.Map<CreateArticleDto>(request);
            var result = await _domainService.AddAsync(createArticleDto);

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ArticleDto>(result);
        }
    }
}