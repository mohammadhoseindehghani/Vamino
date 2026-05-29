using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vamino.Infrastructure.EfCore.DbContexts;

namespace Vamino.Infrastructure.EfCore;

public static class InfrastructureEfCoreServiceRegistrations
{
    public static IServiceCollection ConfigureInfrastructureEfCoreServices(this IServiceCollection services
    , IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });
        return services;
    }
}