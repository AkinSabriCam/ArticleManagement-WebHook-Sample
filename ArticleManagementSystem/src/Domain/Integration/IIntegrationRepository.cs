using System;
using System.Threading.Tasks;
using Common.Repository;

namespace Domain.Integration
{
    public interface IIntegrationRepository : IRepository<Integration, Guid>
    {
        Task<bool> IsExistByCodeAsync(string code);
    }
}