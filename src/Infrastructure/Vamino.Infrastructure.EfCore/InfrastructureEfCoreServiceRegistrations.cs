using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vamino.Infrastructure.EfCore;

public static class InfrastructureEfCoreServiceRegistrations
{
    public static IServiceCollection ConfigureInfrastructureEfCoreServices(this IServiceCollection services
    , IConfiguration configuration)
    {

        return services;
    }
}