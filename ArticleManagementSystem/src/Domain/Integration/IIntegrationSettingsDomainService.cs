using System.Threading.Tasks;
using Domain.Integration.Dtos;

namespace Domain.Integration
{
    public interface IIntegrationSettingsDomainService
    {
        Task<IntegrationSetting> AddAsync(CreateIntegrationSettingsDto dto);
    }
}