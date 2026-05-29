using Vamino.Domain.LoanGuarantorAgg.Enums;

namespace Vamino.Application.Contracts.DTOs.LoanGuarantor;

public record LoanGuarantorDto(
    int Id, 
    int LoanContractId, 
    int UserId,
    GuarantorStatus Status,
    string? Note,
    DateTime? RespondedAt,
    DateTime CreatedAt);