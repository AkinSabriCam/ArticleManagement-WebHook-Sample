using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Integration;
using MediatR;

namespace ArticleApi.Queries.Integration
{
    public class GetAllIntegrationSettingsQueryHandler : IRequestHandler<GetAllIntegrationSettingsQuery, List<IntegrationSettingsDto>>
    {
        private readonly IIntegrationSettingsRepository _repository;
        public GetAllIntegrationSettingsQueryHandler(IIntegrationSettingsRepository repository)
        {
            _repository = repository;
        }

        public Task<List<IntegrationSettingsDto>> Handle(GetAllIntegrationSettingsQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllAsync<IntegrationSettingsDto>();
        }
    }
}