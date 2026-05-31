using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Contracts.Contracts.Repositories;

public interface IUserRepository
{
    Task<UserSearchResultDto?> FindForGuarantorByPhoneAsync(string phoneNumber, CancellationToken ct);
    Task<UserSearchResultDto?> FindForGuarantorByNationalCodeAsync(string nationalCode, CancellationToken ct);
    Task<UserSearchResultDto?> FindForGuarantorByEmailAsync(string email, CancellationToken ct);
    Task<bool> ExistsByIdAsync(int userId, CancellationToken ct);
    Task<PagedResultDto<UserListItemDto>> GetUsersAsync(GetUsersFilterDto filter, CancellationToken ct);
    Task<UserLookupDto?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);
}