using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Infrastructure.EfCore.DbContexts;
using Vamino.Infrastructure.EfCore.Repositories;

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

        services.AddScoped<ILoanContractRepository, LoanContractRepository>();
        services.AddScoped<ILoanGuarantorRepository, LoanGuarantorRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}