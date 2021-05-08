using System.Threading.Tasks;
using Domain.Integration.Dtos;

namespace Domain.Integration
{
    public interface IIntegrationDomainService
    {
        Task<Integration> AddAsync(CreateIntegrationDto dto);
    }
}