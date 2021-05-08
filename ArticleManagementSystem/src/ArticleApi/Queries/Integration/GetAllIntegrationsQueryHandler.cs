using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Integration;
using MediatR;

namespace ArticleApi.Queries.Integration
{
    public class GetAllIntegrationsQueryHandler : IRequestHandler<GetAllIntegrationsQuery, List<IntegrationDto>>
    {
        private readonly IIntegrationRepository _repository;
        public GetAllIntegrationsQueryHandler(IIntegrationRepository repository)
        {
            _repository = repository;
        }

        public Task<List<IntegrationDto>> Handle(GetAllIntegrationsQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllAsync<IntegrationDto>();
        }
    }
}