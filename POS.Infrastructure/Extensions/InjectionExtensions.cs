using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.FileStorage;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Infrastructure.Persistences.Repositories;

namespace POS.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(Pos2Context).Assembly.FullName;

            services.AddDbContext<Pos2Context>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("Pos2Connection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                );

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAzureStorage, AzureStorage>();

            return services;


        }
    }
}