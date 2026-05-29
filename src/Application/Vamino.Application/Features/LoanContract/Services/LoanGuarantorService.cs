using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanContract.Services;

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

    public async Task<List<LoanGuarantorDto>> GetAll(CancellationToken ct)
    {
        return await repo.GetAll(ct);
    }

    public async Task<List<LoanGuarantorDto>> GetAllByUserId(int userId, CancellationToken ct)
    {
        return await repo.GetAllByUserId(userId, ct);
    }
}