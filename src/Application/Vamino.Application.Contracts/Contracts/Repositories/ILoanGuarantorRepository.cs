using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Contracts.Contracts.Repositories;

public interface ILoanGuarantorRepository
{
    Task<int> CreateAsync(CreateLoanGuarantorDto model, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<bool> UpdateNoteAsync(int id, string note, CancellationToken ct);
    Task<bool> ApproveAsync(int id, CancellationToken ct);
    Task<bool> RejectAsync(int id, CancellationToken ct);
    Task<LoanGuarantorDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<LoanGuarantorDto>> GetAll(CancellationToken ct);
    Task<List<LoanGuarantorDto>> GetAllByUserId(int userId, CancellationToken ct);
}