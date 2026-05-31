using Vamino.Domain.LoanContractAgg.Enums;

namespace Vamino.Application.Contracts.DTOs.LoanContract;

public record LoanContractCompletionSummaryDto(
    decimal Amount,
    int ApprovedGuarantorsCount,
    LoanStatus Status);