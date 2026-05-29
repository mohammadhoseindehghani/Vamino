namespace Vamino.Application.Contracts.DTOs.LoanGuarantor;

public record CreateLoanGuarantorDto(int LoanContractId, int UserId, string? Note);