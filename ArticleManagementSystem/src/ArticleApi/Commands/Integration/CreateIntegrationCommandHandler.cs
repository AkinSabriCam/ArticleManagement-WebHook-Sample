using System.Threading;
using System.Threading.Tasks;
using ArticleApi.Queries.Integration;
using Common.UnitOfWork;
using Domain.Integration;
using Domain.Integration.Dtos;
using MediatR;

namespace ArticleApi.Commands.Integration
{
    public class CreateIntegrationCommandHandler : IRequestHandler<CreateIntegrationCommand, IntegrationDto>
    {
        private readonly IIntegrationDomainService _domainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIntegrationCommandHandler(IIntegrationDomainService domainService, IUnitOfWork unitOfWork)
        {
            _domainService = domainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IntegrationDto> Handle(CreateIntegrationCommand request, CancellationToken cancellationToken)
        {

            var dto = new CreateIntegrationDto
            {
                Code = request.Code,
                Url = request.Url,
                EndPoint = request.EndPoint,
                UserName = request.UserName,
                Password = request.Password
            };

            var result = await _domainService.AddAsync(dto);
            await _unitOfWork.SaveChangesAsync();

            return new IntegrationDto
            {
                Id = result.Id,
                Code = result.Code,
                EndPoint = result.EndPoint,
                Url = result.Url,
                UserName = result.UserName,
                Password = result.Password
            };
        }
    }
}