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

    public async Task<LoanContractDto?> GetLoanContract(int id, CancellationToken ct)
    {
        return await repo.GetLoanContract(id, ct);
    }

    public async Task<List<LoanContractDto>> GetAll(CancellationToken ct)
    {
        return await repo.GetAll(ct);
    }

    public async Task<List<LoanContractDto>> GetAllByUserId(int id, CancellationToken ct)
    {
        return await repo.GetAllByUserId(id, ct);
    }

    public async Task<bool> ChangeStatusToCancel(int id, CancellationToken ct)
    {
        return await repo.ChangeStatusToCancel(id, ct);
    }

    public async Task<bool> ChangeStatusToCompleted(int id, CancellationToken ct)
    {
        return await repo.ChangeStatusToCompleted(id, ct);
    }

    public async Task<bool> ChangeStatusToRejected(int id, CancellationToken ct)
    {
        return await repo.ChangeStatusToRejected(id, ct);
    }
}