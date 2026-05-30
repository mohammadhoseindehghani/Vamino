using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Contracts.Contracts.DomainServices;

public interface ILoanGuarantorService
{
    Task<int> CreateAsync(CreateLoanGuarantorDto model, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<bool> UpdateNoteAsync(int id, string note, CancellationToken ct);
    Task<bool> ApproveAsync(int id, CancellationToken ct);
    Task<bool> RejectAsync(int id, CancellationToken ct);
    Task<LoanGuarantorDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<LoanGuarantorDto>> GetAllAsync(CancellationToken ct);
    Task<List<LoanGuarantorDto>> GetAllByUserIdAsync(int userId, CancellationToken ct);
    Task<List<LoanGuarantorDto>> GetAllByContractIdAsync(int contractId, CancellationToken ct);
    Task<bool> CanBeGuarantorForContractAsync(int loanContractId, int userId, CancellationToken ct);
    Task<int?> GetLoanContractIdByGuarantorIdAsync(int guarantorId, CancellationToken ct);
}