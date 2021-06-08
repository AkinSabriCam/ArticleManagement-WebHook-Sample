using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Configurations
{
    public static class IdentityServerService
    {
        public static void AddMyIdentityServer(this IServiceCollection services)
        {
            services
            .AddIdentityServer()
            .AddAspNetIdentity<IdentityUser<Guid>>()
            .AddDeveloperSigningCredential()
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryApiResources(IdentityServerConfig.GetApiResources());
            //.AddProfileService<IdentityProfileService>();
        }
    }
}