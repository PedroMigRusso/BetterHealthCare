using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Application;
using BetterHealthCareAPI.Infrastructure;
using BetterHealthCareAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BetterHealthCareAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BetterHealthCareDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // register generic repository so it can be injected for any entity
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IProcedureService, ProcedureService>();
            services.AddScoped<IPatientActionService, PatientActionService>();
            services.AddScoped<IMedicalFileService, MedicalFileService>();

            services.AddAutoMapper(typeof(Application.Mapper.MappingProfile));

            return services;
        }
    }
}