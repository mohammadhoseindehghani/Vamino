using Vamino.Application.Contracts.DTOs.LoanContract;
using Vamino.Domain.LoanContractAgg.Enums;

namespace Vamino.Application.Contracts.Contracts.Repositories;

public interface ILoanContractRepository
{
    Task<int> CreateLoanContractAsync(CreateLoanContractDto model, CancellationToken ct);
    Task<bool> UpdateLoanContractAsync(int id, UpdateLoanContractDto model, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<LoanContractDto?> GetLoanContractAsync(int id, CancellationToken ct);
    Task<List<LoanContractDto>> GetAllAsync(CancellationToken ct);
    Task<List<LoanContractDto>> GetAllByUserIdAsync(int id, CancellationToken ct);
    Task<bool> ChangeStatusToCancelAsync(int id, CancellationToken ct);
    Task<bool> ChangeStatusToCompletedAsync(int id, CancellationToken ct);
    Task<bool> ChangeStatusToRejectedAsync(int id, CancellationToken ct);
    Task<bool> ChangeStatusToPendingForBankReviewAsync(int id, CancellationToken ct);
    Task<bool> IsEditableAsync(int loanContractId, CancellationToken ct);
    Task<(decimal Amount, int ApprovedGuarantorsCount)> GetContractSummaryForCompletionAsync(int loanContractId, CancellationToken ct);
    Task<LoanContractCompletionSummaryDto?> GetContractSummaryForCompletionProcessAsync(int loanContractId, CancellationToken ct);
    Task<bool> IsOwnerAsync(int loanContractId, int userId, CancellationToken ct);
}