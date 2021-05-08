using System;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Integration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class IntegrationRepository : Repository<Integration, Guid>, IIntegrationRepository
    {
        public IntegrationRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public Task<bool> IsExistByCodeAsync(string code)
        {
            return _dbTable.AsNoTracking().AnyAsync(x => x.Code.Equals(code));
        }
    }
}