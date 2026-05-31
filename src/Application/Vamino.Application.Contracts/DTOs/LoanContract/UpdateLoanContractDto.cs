namespace Vamino.Application.Contracts.DTOs.LoanContract;

public record UpdateLoanContractDto(
    string Title,
    decimal Amount,
    string? Description);