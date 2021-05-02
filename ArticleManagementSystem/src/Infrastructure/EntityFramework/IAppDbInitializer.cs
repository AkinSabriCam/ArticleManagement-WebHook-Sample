using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public interface IAppDbInitializer
    {
        Task MigrateAsync();
    }
}