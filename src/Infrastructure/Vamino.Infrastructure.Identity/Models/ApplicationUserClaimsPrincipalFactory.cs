using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Vamino.Application.Contracts.Contracts.Repositories;

namespace Vamino.Infrastructure.Identity.Models;

public class ApplicationUserClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<IdentityOptions> options,
    IUserRepository userRepository) 
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, options)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        if (!string.IsNullOrWhiteSpace(user.Email))
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        var domainUser = await userRepository.GetByIdentityIdAsync(user.Id);

        if (domainUser is not null)
        {
            identity.AddClaim(new Claim(CustomClaimTypes.DomainUserId, domainUser.Id.ToString()));

            var fullName = $"{domainUser.FirstName} {domainUser.LastName}".Trim();
            if (!string.IsNullOrWhiteSpace(fullName))
                identity.AddClaim(new Claim(CustomClaimTypes.FullName, fullName));
        }

        return identity;
    }
}