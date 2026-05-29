using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;
using Vamino.Application.Contracts.DTOs.Identity;
using Vamino.Domain.UserAgg.Entities;
using Vamino.Infrastructure.EfCore.DbContexts;
using Vamino.Infrastructure.Identity.Context;
using Vamino.Infrastructure.Identity.Models;

namespace Vamino.Infrastructure.Identity.Services;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IdentityDbContext identityDbContext, 
    SignInManager<ApplicationUser> signInManager,
    AppDbContext appDbContext,
    IHttpContextAccessor httpContextAccessor) : IIdentityService
{
    private const string DefaultUserRole = "User";

    public async Task<OperationResultDto> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken = default)
    {
        if (request.Password != request.ConfirmPassword)
            return OperationResultDto.Failure("Passwords do not match.");

        if (await EmailExistsAsync(request.Email, cancellationToken))
            return OperationResultDto.Failure("Email already exists.");

        if (await PhoneExistsAsync(request.PhoneNumber, cancellationToken))
            return OperationResultDto.Failure("Phone number already exists.");

        if (await NationalCodeExistsAsync(request.NationalCode, cancellationToken))
            return OperationResultDto.Failure("National code already exists.");

        await EnsureRoleExistsAsync(DefaultUserRole);

        var identityUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.PhoneNumber,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var identityResult = await userManager.CreateAsync(identityUser, request.Password);

        if (!identityResult.Succeeded)
        {
            return OperationResultDto.Failure(identityResult.Errors.Select(x => x.Description).ToArray());
        }

        await userManager.AddToRoleAsync(identityUser, DefaultUserRole);

        try
        {
            var domainUser = new User
            {
                IdentityId = identityUser.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Balance = 0
            };

            appDbContext.Set<User>().Add(domainUser);

            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await userManager.DeleteAsync(identityUser);
            throw;
        }

        return OperationResultDto.Success();
    }
    public async Task<OperationResultDto> AdminCreateUserAsync(AdminCreateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        if (await EmailExistsAsync(request.Email, cancellationToken))
            return OperationResultDto.Failure("Email already exists.");

        if (await PhoneExistsAsync(request.PhoneNumber, cancellationToken))
            return OperationResultDto.Failure("Phone number already exists.");

        if (await NationalCodeExistsAsync(request.NationalCode, cancellationToken))
            return OperationResultDto.Failure("National code already exists.");

        var roleName = string.IsNullOrWhiteSpace(request.RoleName) ? DefaultUserRole : request.RoleName;

        await EnsureRoleExistsAsync(roleName);

        var identityUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.PhoneNumber,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var createResult = await userManager.CreateAsync(
            identityUser,
            request.Password);

        if (!createResult.Succeeded)
        {
            return OperationResultDto.Failure(createResult.Errors.Select(x => x.Description).ToArray());
        }

        await userManager.AddToRoleAsync(identityUser, roleName);

        try
        {
            var domainUser = new User
            {
                IdentityId = identityUser.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Balance = 0
            };

            appDbContext.Set<User>().Add(domainUser);

            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await userManager.DeleteAsync(identityUser);
            throw;
        }

        return OperationResultDto.Success();
    }
    public async Task<OperationResultDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var identityUser = await userManager.Users
            .SingleOrDefaultAsync(x =>
                    x.Email == request.UserNameOrEmailOrPhone ||
                    x.PhoneNumber == request.UserNameOrEmailOrPhone ||
                    x.UserName == request.UserNameOrEmailOrPhone, cancellationToken);

        if (identityUser is null)
            return OperationResultDto.Failure("Invalid credentials.");

        var result = await signInManager.PasswordSignInAsync(
            identityUser,
            request.Password,
            isPersistent: true,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return OperationResultDto.Failure("Invalid credentials.");

        return OperationResultDto.Success();
    }
    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var identityExists = await userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        var domainExists = await appDbContext.Set<User>().AnyAsync(x => x.Email == email, cancellationToken);
        return identityExists || domainExists;
    }

    public async Task<bool> PhoneExistsAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var identityExists = await userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
        var domainExists = await appDbContext.Set<User>().AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
        return identityExists || domainExists;
    }

    public async Task<bool> NationalCodeExistsAsync(string nationalCode, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<User>().AnyAsync(x => x.NationalCode == nationalCode, cancellationToken);
    }

    public string? GetCurrentUserIdentityId()
    {
        return httpContextAccessor.HttpContext?.User?
            .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        var exists = await roleManager.RoleExistsAsync(roleName);

        if (!exists)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            });
        }
    }
}