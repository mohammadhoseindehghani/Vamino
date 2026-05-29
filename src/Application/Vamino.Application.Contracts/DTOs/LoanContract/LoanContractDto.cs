namespace Vamino.Application.Contracts.DTOs.LoanContract;

public record LoanContractDto(
    int Id,
    string Title,
    int BorrowerId,
    decimal Amount,
    string? Description,
    DateTime CreatedAt);