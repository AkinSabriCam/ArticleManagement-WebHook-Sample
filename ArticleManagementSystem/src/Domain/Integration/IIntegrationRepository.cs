using System;
using Common.Repository;

namespace Domain.Integration
{
    public interface IIntegrationRepository : IRepository<Integration, Guid>
    {
        
    }
}