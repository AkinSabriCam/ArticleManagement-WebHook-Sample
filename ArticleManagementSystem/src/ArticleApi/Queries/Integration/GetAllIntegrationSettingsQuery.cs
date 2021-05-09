using System.Collections.Generic;
using MediatR;

namespace ArticleApi.Queries.Integration
{
    public class GetAllIntegrationSettingsQuery : IRequest<List<IntegrationSettingsDto>>
    {
        
    }
}