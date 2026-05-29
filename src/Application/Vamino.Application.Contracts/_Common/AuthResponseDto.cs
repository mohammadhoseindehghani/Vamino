namespace Vamino.Application.Contracts._Common;

public sealed class AuthResponseDto
{
    public bool Succeeded { get; init; }
    public string? IdentityUserId { get; init; }
    public string? Token { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public string[] Errors { get; init; } = [];

    public static AuthResponseDto Success(string identityUserId, string token, DateTime expiresAt)
        => new()
        {
            Succeeded = true,
            IdentityUserId = identityUserId,
            Token = token,
            ExpiresAt = expiresAt
        };

    public static AuthResponseDto Fail(params string[] errors)
        => new()
        {
            Succeeded = false,
            Errors = errors
        };
}