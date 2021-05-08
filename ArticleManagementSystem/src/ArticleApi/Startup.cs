using ArticleApi.Commands.Article;
using ArticleApi.Configurations;
using Common.UnitOfWork;
using Domain.Article;
using FluentValidation;
using Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ArticleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("Default")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArticleConsumer", Version = "v1" });
            });

            services.AddMediatR(typeof(CreateArticleCommand).Assembly);
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleDomainService, ArticleDomainService>();
            services.AddScoped<IAppDbInitializer, AppDbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPipelineBehavior<,>));
            services.AddValidatorsFromAssemblies(new[] { typeof(CreateArticleCommandValidator).Assembly });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArticleApi v1"));
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.ApplicationServices.Migrate().Wait();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
