using System.Threading;
using System.Threading.Tasks;
using ArticleApi.Queries.Integration;
using Common.UnitOfWork;
using Domain.Integration;
using Domain.Integration.Dtos;
using MapsterMapper;
using MediatR;

namespace ArticleApi.Commands.Integration
{
    public class CreateIntegrationCommandHandler : IRequestHandler<CreateIntegrationCommand, IntegrationDto>
    {
        private readonly IIntegrationDomainService _domainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateIntegrationCommandHandler(IIntegrationDomainService domainService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _domainService = domainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IntegrationDto> Handle(CreateIntegrationCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateIntegrationDto>(request);
            var result = await _domainService.AddAsync(dto);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<IntegrationDto>(result);
        }
    }
}