using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vamino.Application.Behavior;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Features.LoanContract.Services;
using Vamino.Application.Features.LoanGuarantor.Services;
using Vamino.Application.Features.User.Services;

namespace Vamino.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<ILoanContractService, LoanContractService>();
        services.AddScoped<ILoanGuarantorService, LoanGuarantorService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}