using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;
using Vamino.Infrastructure.Identity.Context;
using Vamino.Infrastructure.Identity.Models;
using Vamino.Infrastructure.Identity.Services;

namespace Vamino.Infrastructure.Identity;

public static class InfrastructureIdentityServiceRegistrations
{
    public static IServiceCollection ConfigureInfrastructureIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });

        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // User
                options.User.RequireUniqueEmail = true;

                // SignIn
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "Vamino.Auth";

            options.LoginPath = "/Auth/Login";

            options.LogoutPath = "/Auth/Logout";

            options.AccessDeniedPath = "/Auth/AccessDenied";

            options.ExpireTimeSpan = TimeSpan.FromDays(7);

            options.SlidingExpiration = true;

            options.Cookie.HttpOnly = true;

            options.Cookie.SameSite = SameSiteMode.Lax;

            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        services.AddHttpContextAccessor();

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}