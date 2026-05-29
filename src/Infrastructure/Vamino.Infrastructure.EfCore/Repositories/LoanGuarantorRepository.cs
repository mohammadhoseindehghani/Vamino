using Microsoft.EntityFrameworkCore;
using Vamino.Application.Contracts.Contracts.Repositories;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;
using Vamino.Domain.LoanGuarantorAgg.Entities;
using Vamino.Domain.LoanGuarantorAgg.Enums;
using Vamino.Infrastructure.EfCore.DbContexts;

namespace Vamino.Infrastructure.EfCore.Repositories;

public class LoanGuarantorRepository(AppDbContext context) : ILoanGuarantorRepository
{
    public async Task<int> CreateAsync(CreateLoanGuarantorDto model, CancellationToken ct)
    {
        var loanGuarantor = new LoanGuarantor()
        {
            UserId = model.UserId,
            LoanContractId = model.LoanContractId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            GuarantorStatus = GuarantorStatus.Pending,
            Note = model.Note
        };

        context.Add(loanGuarantor);
        await context.SaveChangesAsync(ct);
        return loanGuarantor.Id;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanGuarantors.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.GuarantorStatus, GuarantorStatus.Cancelled )
                .SetProperty(x => x.IsDeleted, true)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow)
                .SetProperty(x => x.DeletedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> UpdateNoteAsync(int id, string note, CancellationToken ct)
    {
        var effectedRows = await context.LoanGuarantors.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.Note, note)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> ApproveAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanGuarantors.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.GuarantorStatus, GuarantorStatus.Approved)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<bool> RejectAsync(int id, CancellationToken ct)
    {
        var effectedRows = await context.LoanGuarantors.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.GuarantorStatus, GuarantorStatus.Rejected)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        return effectedRows > 0;
    }

    public async Task<LoanGuarantorDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.LoanGuarantors.Where(x => x.Id == id)
            .AsNoTracking()
            .Select(x => new LoanGuarantorDto(
                x.Id, 
                x.LoanContractId,
                x.UserId,
                x.GuarantorStatus,
                x.Note,
                x.RespondedAt,
                x.CreatedAt
            )).FirstOrDefaultAsync(ct);
    }

    public async Task<List<LoanGuarantorDto>> GetAll(CancellationToken ct)
    {
        return await context.LoanGuarantors
            .AsNoTracking()
            .Select(x => new LoanGuarantorDto(
                x.Id,
                x.LoanContractId,
                x.UserId,
                x.GuarantorStatus,
                x.Note,
                x.RespondedAt,
                x.CreatedAt
            )).ToListAsync(ct);
    }

    public async Task<List<LoanGuarantorDto>> GetAllByUserId(int userId, CancellationToken ct)
    {
        return await context.LoanGuarantors
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .OrderByDescending(x => x.GuarantorStatus == GuarantorStatus.Pending)
            .ThenByDescending(x => x.CreatedAt) 
            .Select(x => new LoanGuarantorDto(
                x.Id,
                x.LoanContractId,
                x.UserId,
                x.GuarantorStatus,
                x.Note,
                x.RespondedAt,
                x.CreatedAt
            ))
            .ToListAsync(ct);
    }
}