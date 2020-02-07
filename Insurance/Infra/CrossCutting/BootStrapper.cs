using Insurance.Application.Interfaces;
using Insurance.Core.Interfaces;
using Insurance.Domain.Interfaces.Repository;
using Insurance.Domain.Interfaces.Service;
using Insurance.Domain.Services;
using Insurance.Infra.Data;
using Insurance.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Infra.CrossCutting
{
    [ExcludeFromCodeCoverage]
    public static class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            // Inject Infra
            services.AddDbContext<InsuranceDb>(opt => opt.UseInMemoryDatabase(nameof(Insurance)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Inject AppServices
            services.AddScoped<IAdvisorAppService, AdvisorAppService>();
            services.AddScoped<ICarrierAppService, CarrierAppService>();
            services.AddScoped<IMgaAppService, MgaAppService>();
            services.AddScoped<IContractAppService, ContractAppService>();

            // Inject Services
            services.AddScoped<IPathFinderService, PathFinderService>();

            // Inject Repository
            services.AddScoped<IAdvisorRepository, AdvisorRepository>();
            services.AddScoped<ICarrierRepository, CarrierRepository>();
            services.AddScoped<IMgaRepository, MgaRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractPartRepository, ContractPartRepository>();
        }
    }
}