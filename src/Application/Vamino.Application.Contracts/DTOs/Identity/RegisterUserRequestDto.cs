namespace Vamino.Application.Contracts.DTOs.Identity;

public sealed record RegisterUserRequestDto(
    string FirstName,
    string LastName,
    string NationalCode,
    string PhoneNumber,
    string Email,
    string Password,
    string ConfirmPassword
);