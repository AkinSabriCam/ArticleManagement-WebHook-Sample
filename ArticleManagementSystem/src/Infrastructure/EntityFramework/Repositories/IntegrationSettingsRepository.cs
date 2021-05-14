using System;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Integration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class IntegrationSettingsRepository : Repository<IntegrationSetting, Guid>, IIntegrationSettingsRepository
    {
        public IntegrationSettingsRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public Task<IntegrationSetting> GetByCodeAsync(string code)
        {
            return _dbTable.AsNoTracking().FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        public Task<bool> IsExistAsync(string code)
        {
            return _dbTable.AsNoTracking().AnyAsync(x => x.Code.Equals(code));
        }
    }
}