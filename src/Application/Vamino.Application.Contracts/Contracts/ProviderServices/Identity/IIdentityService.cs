using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.Identity;

namespace Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

public interface IIdentityService
{
    Task<OperationResultDto> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken = default);
    Task<OperationResultDto> AdminCreateUserAsync(AdminCreateUserRequestDto request, CancellationToken cancellationToken = default);
    Task<OperationResultDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task LogoutAsync();
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> PhoneExistsAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<bool> NationalCodeExistsAsync(string nationalCode, CancellationToken cancellationToken = default);
    string? GetCurrentUserIdentityId();
}