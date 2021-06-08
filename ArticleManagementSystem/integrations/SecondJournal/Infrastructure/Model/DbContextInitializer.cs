using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Model
{
    public class DbContextInitializer : IDbContextInitializer
    {
        private readonly AppDbContext _dbContext;

        public DbContextInitializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task MigrateAsync()
        {
            return _dbContext.Database.MigrateAsync();
        }
    }
}