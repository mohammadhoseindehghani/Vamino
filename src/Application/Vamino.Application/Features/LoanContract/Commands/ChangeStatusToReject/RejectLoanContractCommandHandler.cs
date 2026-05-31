using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToReject;

public class RejectLoanContractCommandHandler(ILoanContractService loanContractService)
    : IRequestHandler<RejectLoanContractCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RejectLoanContractCommand request, CancellationToken ct)
    {
        var ok = await loanContractService.ChangeStatusToRejectedAsync(request.Id, ct);
        return ok
            ? Result<bool>.Success(true, "درخواست وام رد شد.")
            : Result<bool>.Failure("رد کردن درخواست وام با شکست مواجه شد.");
    }
}
