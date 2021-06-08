using System.Threading.Tasks;
namespace Infrastructure.Model
{
    public interface IDbContextInitializer
    {
        Task MigrateAsync();
    }
}