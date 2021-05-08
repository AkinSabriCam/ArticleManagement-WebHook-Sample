using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Integration.Dtos;

namespace Domain.Integration
{
    public class IntegrationDomainService : IIntegrationDomainService
    {
        private readonly IIntegrationRepository _repository;

        public IntegrationDomainService(IIntegrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Integration> AddAsync(CreateIntegrationDto dto)
        {
            if (await _repository.IsExistByCodeAsync(dto.Code))
                throw new ValidationException("There is already integration with same code!");

            var entity = new Integration();
            SetEditableFields(entity, dto);

            _repository.Add(entity);
            return entity;
        }

        private static void SetEditableFields(Integration integration, CreateIntegrationDto dto)
        {
            integration.SetCode(dto.Code);
            integration.SetEndPoint(dto.EndPoint);
            integration.SetUrl(dto.Url);
            integration.SetUserName(dto.UserName);
            integration.SetPassword(dto.Password);
        }
    }
}