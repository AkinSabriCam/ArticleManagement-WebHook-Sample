using System.Threading.Tasks;
using Domain.Article.Dtos;
using FluentValidation;

namespace Domain.Article
{
    public class ArticleDomainService : IArticleDomainService
    {
        private readonly IArticleRepository _repository;

        public ArticleDomainService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Article> AddAsync(CreateArticleDto dto)
        {
            if (await _repository.AnyAsync(x => x.Code == dto.Code))
                throw new ValidationException("There is already article with same code!");

            var entity = new Article();
            SetEditableFields(dto, entity);

            // entity.Created()

            _repository.Add(entity);
            return entity;
        }

        private static void SetEditableFields(CreateArticleDto dto, Article entity)
        {
            entity.SetAuthor(dto.AuthorName, dto.AuthorLastName);
            entity.SetCode(dto.Code);
            entity.SetContent(dto.Content);
            entity.SetTitle(dto.Title);
            entity.Category = dto.Category;
        }
    }
}