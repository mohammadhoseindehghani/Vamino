namespace Vamino.Application.Contracts.DTOs.LoanContract;

public record CreateLoanContractDto(
    string Title,
    int BorrowerId,
    decimal Amount,
    string? Description);