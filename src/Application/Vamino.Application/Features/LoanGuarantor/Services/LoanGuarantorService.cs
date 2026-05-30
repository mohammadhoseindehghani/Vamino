using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Services;

public class LoanGuarantorService(ILoanGuarantorRepository repo) : ILoanGuarantorService
{
    public async Task<int> CreateAsync(CreateLoanGuarantorDto model, CancellationToken ct)
    {
        return await repo.CreateAsync(model, ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return await repo.DeleteAsync(id, ct);
    }

    public async Task<bool> UpdateNoteAsync(int id, string note, CancellationToken ct)
    {
        return await repo.UpdateNoteAsync(id, note, ct);
    }

    public async Task<bool> ApproveAsync(int id, CancellationToken ct)
    {
        return await repo.ApproveAsync(id, ct);
    }

    public async Task<bool> RejectAsync(int id, CancellationToken ct)
    {
        return await repo.RejectAsync(id, ct);
    }

    public async Task<LoanGuarantorDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await repo.GetByIdAsync(id, ct);
    }

    public async Task<List<LoanGuarantorDto>> GetAllAsync(CancellationToken ct)
    {
        return await repo.GetAllAsync(ct);
    }

    public async Task<List<LoanGuarantorDto>> GetAllByUserIdAsync(int userId, CancellationToken ct)
    {
        return await repo.GetAllByUserIdAsync(userId, ct);
    }

    public async Task<List<LoanGuarantorDto>> GetAllByContractIdAsync(int contractId, CancellationToken ct)
    {
        return await repo.GetAllByContractIdAsync(contractId, ct);
    }

    public async Task<bool> CanBeGuarantorForContractAsync(int loanContractId, int userId, CancellationToken ct)
    {
        return await repo.CanBeGuarantorForContractAsync(loanContractId, userId, ct);
    }
}