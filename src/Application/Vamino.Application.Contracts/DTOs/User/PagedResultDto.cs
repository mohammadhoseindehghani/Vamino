namespace Vamino.Application.Contracts.DTOs.User;

public record PagedResultDto<T>(
    List<T> Items,
    int Page,
    int PageSize,
    int TotalCount);
