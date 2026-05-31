using System.Security.Claims;
using Vamino.Infrastructure.Identity.Models;

namespace Vamino.Infrastructure.Identity.Extension;

public static class ClaimsPrincipalExtensions
{
    public static int? GetDomainUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(CustomClaimTypes.DomainUserId);

        return int.TryParse(value, out var id) ? id : null;
    }
}