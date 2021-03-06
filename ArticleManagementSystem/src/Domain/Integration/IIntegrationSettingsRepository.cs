using System;
using System.Threading.Tasks;
using Common.Repository;

namespace Domain.Integration
{
    public interface IIntegrationSettingsRepository : IRepository<IntegrationSetting, Guid>
    {
        Task<bool> IsExistAsync(string code);
        Task<IntegrationSetting> GetByCodeAsync(string code);
    }
}