using Microsoft.EntityFrameworkCore;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.LoanContract;
using Vamino.Domain.LoanContractAgg.Entities;
using Vamino.Domain.LoanContractAgg.Enums;
using Vamino.Domain.LoanGuarantorAgg.Enums;
using Vamino.Infrastructure.EfCore.DbContexts;

namespace Vamino.Infrastructure.EfCore.Repositories;

public class LoanContractRepository(AppDbContext context) : ILoanContractRepository
{
    public async Task<int> CreateLoanContractAsync(CreateLoanContractDto model, CancellationToken ct)
    {
        var loanContract = new LoanContract()
        {
            Title = model.Title,
            Amount = model.Amount,
            BorrowerId = model.BorrowerId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            Description = model.Description,
            LoanStatus = LoanStatus.PendingForGuarantors
        };
        context.Add(loanContract);
        await context.SaveChangesAsync(ct);
        return loanContract.Id;
    }

    public async Task<bool> UpdateLoanContractAsync(int id, UpdateLoanContractDto model, CancellationToken ct)
    {
        var effectedRows = await context.LoanContracts.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.Title, model.Title)
                .SetProperty(x => x.Amount, model.Amount)
                .SetProperty(x => x.Description, model.Description), ct);

        return effectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanContracts.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.LoanStatus, LoanStatus.Cancelled)
                .SetProperty(x => x.IsDeleted, true)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow)
                .SetProperty(x => x.DeletedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<LoanContractDto?> GetLoanContractAsync(int id, CancellationToken ct)
    {
        return await context.LoanContracts
            .Where(x => x.Id == id)
            .AsNoTracking()
            .Select(x => new LoanContractDto(
                x.Id,
                x.Title,
                x.BorrowerId,
                x.Amount,
                x.Description,
                x.LoanStatus,
                x.CreatedAt
            )).FirstOrDefaultAsync(ct);
    }

    public async Task<List<LoanContractDto>> GetAllAsync(CancellationToken ct)
    {
        return await context.LoanContracts
            .AsNoTracking()
            .Select(x => new LoanContractDto(
                x.Id,
                x.Title,
                x.BorrowerId,
                x.Amount,
                x.Description,
                x.LoanStatus,
                x.CreatedAt
            )).ToListAsync(ct);
    }

    public async Task<List<LoanContractDto>> GetAllByUserIdAsync(int id, CancellationToken ct)
    {
        return await context.LoanContracts
            .Where(x => x.BorrowerId == id)
            .AsNoTracking()
            .Select(x => new LoanContractDto(
                x.Id,
                x.Title,
                x.BorrowerId,
                x.Amount,
                x.Description,
                x.LoanStatus,
                x.CreatedAt
            )).ToListAsync(ct);
    }

    public async Task<bool> ChangeStatusToCancelAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanContracts.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.LoanStatus, LoanStatus.Cancelled)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> ChangeStatusToCompletedAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanContracts.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.LoanStatus, LoanStatus.Completed)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> ChangeStatusToRejectedAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanContracts.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.LoanStatus, LoanStatus.Rejected)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> ChangeStatusToPendingForBankReviewAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanContracts.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.LoanStatus, LoanStatus.PendingForBankReview)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> IsEditableAsync(int loanContractId, CancellationToken ct)
    {
        return await context.LoanContracts
            .AnyAsync(x =>
                    x.Id == loanContractId &&
                    x.LoanStatus == LoanStatus.PendingForGuarantors, ct);
    }

    public async Task<(decimal Amount, int ApprovedGuarantorsCount)> GetContractSummaryForCompletionAsync(
        int loanContractId,
        CancellationToken ct)
    {
        var result = await context.LoanContracts
            .Where(x => x.Id == loanContractId)
            .Select(x => new
            {
                x.Amount,
                ApprovedGuarantorsCount = x.LoanGuarantors.Count(g => g.GuarantorStatus == GuarantorStatus.Approved)
            }).FirstOrDefaultAsync(ct);

        return result is null ? (0, 0) : (result.Amount, result.ApprovedGuarantorsCount);
    }

    public async Task<LoanContractCompletionSummaryDto?> GetContractSummaryForCompletionProcessAsync(int loanContractId, CancellationToken ct)
    {
        return await context.LoanContracts
            .Where(x => x.Id == loanContractId)
            .Select(x => new LoanContractCompletionSummaryDto(
                 x.Amount,
                 x.LoanGuarantors.Count(g => g.GuarantorStatus == GuarantorStatus.Approved),
                 x.LoanStatus
            )).FirstOrDefaultAsync(ct);
    }


    public async Task<bool> IsOwnerAsync(int loanContractId, int userId, CancellationToken ct)
    {
        return await context.LoanContracts
            .AnyAsync(x =>
                    x.Id == loanContractId &&
                    x.BorrowerId == userId, ct);
    }
}