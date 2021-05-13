using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer.Configurations
{
    public class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients()
        {

            return new Client[]
            {
                new Client
                {
                    ClientId = "sj.api.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenLifetime = 36000,
                    IdentityTokenLifetime = 36000,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowedScopes = { "sj.api.scope" },
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientSecrets = {new Secret("sj.api.secret".Sha256())},
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("sj.api.scope"),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("sj.api.resource") {Scopes = {"sj.api.scope"}},
            };
        }
    }
}