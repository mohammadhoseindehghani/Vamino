using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToCancel;

public class CancelLoanContractCommandHandler(ILoanContractService loanContractService)
    : IRequestHandler<CancelLoanContractCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CancelLoanContractCommand request, CancellationToken ct)
    {
        var editable = await loanContractService.IsEditableAsync(request.Id, ct);
        if (!editable)
            return Result<bool>.Failure("این درخواست وام قابل لغو نیست.");

        var ok = await loanContractService.ChangeStatusToCancelAsync(request.Id, ct);
        return ok
            ? Result<bool>.Success(true, "درخواست وام با موفقیت لغو شد.")
            : Result<bool>.Failure("لغو درخواست وام با شکست مواجه شد.");
    }
}
