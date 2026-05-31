namespace Vamino.Application.Contracts.DTOs.User;

public record GetUsersFilterDto(
    string? Search,       
    int Page = 1,
    int PageSize = 10);
