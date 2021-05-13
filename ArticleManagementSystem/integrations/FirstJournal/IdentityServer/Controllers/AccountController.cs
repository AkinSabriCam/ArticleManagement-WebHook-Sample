using System.Threading.Tasks;
using IdentityServer.Configurations;
using IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly string _authorityAddress;

        public AccountController(IIdentityService identityService, IOptions<AuthoritySettings> authoritySettings)
        {
            _identityService = identityService;
            _authorityAddress = authoritySettings.Value.Url;
        }


        [HttpPost]
        public async Task<ActionResult<TokenResponse>> GetToken(TokenRequest request)
        {
            return Ok(await _identityService.GetToken(request, _authorityAddress));
        }
    }
}