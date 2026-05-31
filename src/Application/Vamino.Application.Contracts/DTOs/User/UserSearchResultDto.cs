namespace Vamino.Application.Contracts.DTOs.User;

public record UserSearchResultDto(
    int Id,
    string FullName,
    string NationalCode,
    string PhoneNumber,
    string Email);