using Insurance.Api.Middlewares;
using Insurance.Core.Domain.Core;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Domain.Services;
using Insurance.Core.Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Insurance.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static readonly string CorsPolicy = "AllowAllOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            Register(services);

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Insurance API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicy);

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalErrorMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void Register(IServiceCollection services)
        {
            services.AddDbContext<InsuranceDb>(opt => opt.UseInMemoryDatabase("Insurance"));

            services.AddScoped<IAdvisorService, AdvisorService>();
            services.AddScoped<ICarrierService, CarrierService>();
            services.AddScoped<IMgaService, MgaService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IPathFinder, PathFinder>();
        }
    }
}