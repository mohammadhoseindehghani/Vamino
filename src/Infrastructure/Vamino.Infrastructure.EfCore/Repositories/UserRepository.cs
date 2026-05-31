using Microsoft.EntityFrameworkCore;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.User;
using Vamino.Infrastructure.EfCore.DbContexts;

namespace Vamino.Infrastructure.EfCore.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public Task<bool> ExistsByIdAsync(int userId, CancellationToken ct)
    {
        return context.Users.AnyAsync(x => x.Id == userId && !x.IsDeleted, ct);
    }

    public async Task<UserSearchResultDto?> FindForGuarantorByPhoneAsync(string phoneNumber, CancellationToken ct)
    {
        phoneNumber = NormalizePhone(phoneNumber);

        return await context.Users
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.PhoneNumber == phoneNumber)
            .Select(x => new UserSearchResultDto(
                x.Id,
                (x.FirstName + " " + x.LastName).Trim(),
                x.NationalCode,
                x.PhoneNumber,
                x.Email
            ))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<UserSearchResultDto?> FindForGuarantorByNationalCodeAsync(string nationalCode, CancellationToken ct)
    {
        nationalCode = NormalizeNationalCode(nationalCode);

        return await context.Users
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.NationalCode == nationalCode)
            .Select(x => new UserSearchResultDto(
                x.Id,
                (x.FirstName + " " + x.LastName).Trim(),
                x.NationalCode,
                x.PhoneNumber,
                x.Email
            ))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<UserSearchResultDto?> FindForGuarantorByEmailAsync(string email, CancellationToken ct)
    {
        email = (email ?? "").Trim().ToLowerInvariant();

        return await context.Users
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.Email.ToLower() == email)
            .Select(x => new UserSearchResultDto(
                x.Id,
                (x.FirstName + " " + x.LastName).Trim(),
                x.NationalCode,
                x.PhoneNumber,
                x.Email
            ))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResultDto<UserListItemDto>> GetUsersAsync(GetUsersFilterDto filter, CancellationToken ct)
    {
        var page = filter.Page < 1 ? 1 : filter.Page;
        var pageSize = filter.PageSize is < 1 or > 200 ? 20 : filter.PageSize;

        var query = context.Users
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var s = filter.Search.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(s) ||
                x.LastName.Contains(s) ||
                x.PhoneNumber.Contains(s) ||
                x.NationalCode.Contains(s) ||
                x.Email.Contains(s));
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UserListItemDto(
                x.Id,
                x.FirstName,
                x.LastName,
                x.NationalCode,
                x.PhoneNumber,
                x.Email
            ))
            .ToListAsync(ct);

        return new PagedResultDto<UserListItemDto>(items, page, pageSize, total);
    }

    public async Task<UserLookupDto?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserLookupDto(u.Id, u.FirstName, u.LastName))
            .FirstOrDefaultAsync(cancellationToken);
    }

    private static string NormalizePhone(string phone)
        => (phone ?? "").Trim();

    private static string NormalizeNationalCode(string code)
        => (code ?? "").Trim();
}