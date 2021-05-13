using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IIdentityService
    {
        Task<TokenResponse> GetToken(TokenRequest request, string address);
    }
}