using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Approve;

public class ApproveLoanGuarantorCommandHandler(
    ILoanGuarantorService loanGuarantorService,
    ILoanContractService loanContractService)
    : IRequestHandler<ApproveLoanGuarantorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ApproveLoanGuarantorCommand request, CancellationToken ct)
    {
        var loanContractId = await loanGuarantorService.GetLoanContractIdByGuarantorIdAsync(request.Id, ct);
        if (!loanContractId.HasValue)
        {
            return Result<bool>.Failure("درخواست ضمانت یافت نشد.", ErrorCodes.NotFound, false);
        }

        var ok = await loanGuarantorService.ApproveAsync(request.Id, ct);

        if (!ok)
        {
            return Result<bool>.Failure("تایید درخواست ضمانت با شکست مواجه شد.", ErrorCodes.OperationFailed, false);
        }

        await loanContractService.EvaluateStatusAfterGuarantorResponseAsync(loanContractId.Value, ct);

        return Result<bool>.Success(true, "درخواست ضمانت تایید شد.");
    }
}
