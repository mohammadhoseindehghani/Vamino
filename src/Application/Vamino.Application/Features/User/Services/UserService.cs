using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Services;

public class UserService(IUserRepository repo) : IUserService
{
    public async Task<UserSearchResultDto?> FindForGuarantorByPhoneAsync(string phoneNumber, CancellationToken ct)
    {
        return await repo.FindForGuarantorByPhoneAsync(phoneNumber, ct);
    }

    public async Task<UserSearchResultDto?> FindForGuarantorByNationalCodeAsync(string nationalCode, CancellationToken ct)
    {
        return await repo.FindForGuarantorByNationalCodeAsync(nationalCode, ct);
    }

    public async Task<UserSearchResultDto?> FindForGuarantorByEmailAsync(string email, CancellationToken ct)
    {
        return await repo.FindForGuarantorByEmailAsync(email, ct);
    }

    public async Task<bool> ExistsByIdAsync(int userId, CancellationToken ct)
    {
        return await repo.ExistsByIdAsync(userId, ct);
    }

    public async Task<PagedResultDto<UserListItemDto>> GetUsersAsync(GetUsersFilterDto filter, CancellationToken ct)
    {
        return await repo.GetUsersAsync(filter, ct);
    }
}