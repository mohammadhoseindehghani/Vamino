using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.LoanContract;

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
}