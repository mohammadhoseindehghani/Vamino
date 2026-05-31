using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.LoanContract;
using Vamino.Domain.LoanContractAgg.Enums;

namespace Vamino.Application.Features.LoanContract.Services;

public class LoanContractService(ILoanContractRepository repo) : ILoanContractService
{
    public async Task<int> CreateLoanContractAsync(CreateLoanContractDto model, CancellationToken ct)
    {
        return await repo.CreateLoanContractAsync(model, ct);
    }

    public async Task<bool> UpdateLoanContractAsync(int id, UpdateLoanContractDto model, CancellationToken ct)
    {
        return await repo.UpdateLoanContractAsync(id, model, ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return await repo.DeleteAsync(id, ct);
    }

    public async Task<LoanContractDto?> GetLoanContractAsync(int id, CancellationToken ct)
    {
        return await repo.GetLoanContractAsync(id, ct);
    }

    public async Task<List<LoanContractDto>> GetAllAsync(CancellationToken ct)
    {
        return await repo.GetAllAsync(ct);
    }

    public async Task<List<LoanContractDto>> GetAllByUserIdAsync(int id, CancellationToken ct)
    {
        return await repo.GetAllByUserIdAsync(id, ct);
    }

    public async Task<bool> ChangeStatusToCancelAsync(int id, CancellationToken ct)
    {
        return await repo.ChangeStatusToCancelAsync(id, ct);
    }

    public async Task<bool> ChangeStatusToCompletedAsync(int id, CancellationToken ct)
    {
        return await repo.ChangeStatusToCompletedAsync(id, ct);
    }

    public async Task<bool> ChangeStatusToRejectedAsync(int id, CancellationToken ct)
    {
        return await repo.ChangeStatusToRejectedAsync(id, ct);
    }

    public async Task<bool> IsEditableAsync(int loanContractId, CancellationToken ct)
    {
        return await repo.IsEditableAsync(loanContractId, ct);
    }

    public async Task<(decimal Amount, int ApprovedGuarantorsCount)> GetContractSummaryForCompletionAsync(int loanContractId, CancellationToken ct)
    {
        return await repo.GetContractSummaryForCompletionAsync(loanContractId, ct);
    }

    public async Task<bool> IsOwnerAsync(int loanContractId, int userId, CancellationToken ct)
    {
        return await repo.IsOwnerAsync(loanContractId, userId, ct);
    }

    public async Task EvaluateStatusAfterGuarantorResponseAsync(int loanContractId, CancellationToken ct)
    {
        var summary = await repo.GetContractSummaryForCompletionProcessAsync(loanContractId, ct);

        if (summary is null)
            return;

        if (summary.Status != LoanStatus.PendingForGuarantors)
            return;

        var requiredGuarantors = Math.Max(1, (int)Math.Ceiling(summary.Amount / 50_000_000m));

        if (summary.ApprovedGuarantorsCount >= requiredGuarantors)
        {
            await repo.ChangeStatusToPendingForBankReviewAsync(loanContractId, ct);
        }
    }
}