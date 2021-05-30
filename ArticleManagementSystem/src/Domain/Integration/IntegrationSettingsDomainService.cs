using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Integration.Dtos;

namespace Domain.Integration
{
    public class IntegrationSettingsDomainService : IIntegrationSettingsDomainService
    {
        private readonly IIntegrationSettingsRepository _repository;

        public IntegrationSettingsDomainService(IIntegrationSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IntegrationSetting> AddAsync(CreateIntegrationSettingsDto dto)
        {
            if (await _repository.IsExistAsync(dto.Code))
                throw new ValidationException("There is already integration with same code!");

            var entity = new IntegrationSetting();
            SetEditableFields(entity, dto);

            _repository.Add(entity);
            return entity;
        }

        private static void SetEditableFields(IntegrationSetting integration, CreateIntegrationSettingsDto dto)
        {
            integration.SetCode(dto.Code);
            integration.SetUrl(dto.Url);
            integration.SetEndPoint(dto.EndPoint);
            integration.SetTokenUrl(dto.TokenUrl);
            integration.SetTokenEndPoint(dto.TokenEndPoint);
            integration.SetUserName(dto.UserName);
            integration.SetPassword(dto.Password);
            integration.SetClientId(dto.ClientId);
            integration.SetClientSecret(dto.ClientSecret);
        }
    }
}