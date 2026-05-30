namespace Vamino.Application.Contracts.DTOs.User;

public record UserListItemDto(
    int Id,
    string FirstName,
    string LastName,
    string NationalCode,
    string PhoneNumber,
    string Email);
