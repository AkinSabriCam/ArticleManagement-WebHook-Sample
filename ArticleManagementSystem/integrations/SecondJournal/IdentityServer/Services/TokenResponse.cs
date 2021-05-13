using System;
namespace IdentityServer.Services
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }


    }
}