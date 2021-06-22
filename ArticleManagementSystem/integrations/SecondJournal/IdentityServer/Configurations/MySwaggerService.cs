using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace IdentityServer.Configurations
{
    public static class MySwaggerService
    {
        public static void AddMySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Second Journal IdentityServer", Version = "v1" });
            });
        }
    }
}