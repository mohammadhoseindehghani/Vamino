using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanContract.Commands.Delete;

public class DeleteLoanContractCommandHandler(ILoanContractService loanContractService)
    : IRequestHandler<DeleteLoanContractCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteLoanContractCommand request, CancellationToken ct)
    {
        var editable = await loanContractService.IsEditableAsync(request.Id, ct);
        if (!editable)
            return Result<bool>.Failure("این درخواست وام قابل حذف نیست.");

        var ok = await loanContractService.DeleteAsync(request.Id, ct);
        return ok
            ? Result<bool>.Success(true,"درخواست وام با موفقیت حذف شد.")
            : Result<bool>.Failure("حذف درخواست وام با شکست مواجه شد.");
    }
}
