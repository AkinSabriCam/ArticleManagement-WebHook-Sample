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
    public class CreateIntegrationSettingsCommandHandler : IRequestHandler<CreateIntegrationSettingsCommand, IntegrationSettingsDto>
    {
        private readonly IIntegrationSettingsDomainService _domainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateIntegrationSettingsCommandHandler(IIntegrationSettingsDomainService domainService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _domainService = domainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IntegrationSettingsDto> Handle(CreateIntegrationSettingsCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateIntegrationSettingsDto>(request);
            var result = await _domainService.AddAsync(dto);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<IntegrationSettingsDto>(result);
        }
    }
}