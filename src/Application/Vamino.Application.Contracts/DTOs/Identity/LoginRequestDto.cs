namespace Vamino.Application.Contracts.DTOs.Identity;


public sealed record LoginRequestDto(
    string UserNameOrEmailOrPhone,
    string Password
);