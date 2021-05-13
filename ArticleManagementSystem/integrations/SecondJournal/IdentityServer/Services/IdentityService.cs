using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<TokenResponse> GetToken(TokenRequest request, string address)
        {
            var discoResponse = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Policy =
                {
                    RequireHttps = false,
                    ValidateIssuerName = false,
                    ValidateEndpoints = false,
                },
                Address = address
            });

            if (discoResponse.IsError)
                throw new Exception(discoResponse.Error);


            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoResponse.TokenEndpoint,
                Password = request.Password,
                UserName = request.Username,
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret
            });

            if (tokenResponse.IsError)
                throw new Exception(tokenResponse.Error);


            return new TokenResponse
            {
                Token = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                ExpiresIn = tokenResponse.ExpiresIn
            };
        }
    }
}