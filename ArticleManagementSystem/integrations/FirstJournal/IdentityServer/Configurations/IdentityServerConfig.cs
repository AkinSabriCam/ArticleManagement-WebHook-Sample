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
                    ClientId = "fj.api.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenLifetime = 36000,
                    IdentityTokenLifetime = 36000,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowedScopes = { "fj.api.scope" },
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientSecrets = {new Secret("fj.api.secret".Sha256())},
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("fj.api.scope"),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("fj.api.resource") {Scopes = {"fj.api.scope"}},
            };
        }
    }
}