using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Contracts.Contracts.DomainServices;

public interface ILoanContractService
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
}