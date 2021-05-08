using System.Collections.Generic;
using MediatR;

namespace ArticleApi.Queries.Integration
{
    public class GetAllIntegrationsQuery : IRequest<List<IntegrationDto>>
    {
        
    }
}