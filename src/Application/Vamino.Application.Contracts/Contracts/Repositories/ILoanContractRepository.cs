using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Contracts.Contracts.Repositories;

public interface ILoanContractRepository
{
    Task<int> CreateLoanContractAsync(CreateLoanContractDto model, CancellationToken ct);
    Task<bool> UpdateLoanContractAsync(int id, UpdateLoanContractDto model, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<LoanContractDto?> GetLoanContract(int id, CancellationToken ct);
    Task<List<LoanContractDto>> GetAll(CancellationToken ct);
    Task<List<LoanContractDto>> GetAllByUserId(int id, CancellationToken ct);
    Task<bool> ChangeStatusToCancel(int id, CancellationToken ct);
    Task<bool> ChangeStatusToCompleted(int id, CancellationToken ct);
    Task<bool> ChangeStatusToRejected(int id, CancellationToken ct);
}