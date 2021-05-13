using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Model;
using MediatR;

namespace Api.Commands
{
    public class CreateArticleCommandHandler : AsyncRequestHandler<CreateArticleCommand>
    {
        public readonly AppDbContext _dbContext;

        public CreateArticleCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Article
                {
                    Code = request.Code,
                    Content = request.Content,
                    Title = request.Title,
                    Category = request.Category,
                    AuthorName = request.AuthorName,
                    AuthorLastName = request.AuthorLastName
                };

                await _dbContext.Articles.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}