using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Approve;

public class ApproveLoanGuarantorCommandHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<ApproveLoanGuarantorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ApproveLoanGuarantorCommand request, CancellationToken ct)
    {
        var ok = await loanGuarantorService.ApproveAsync(request.Id, ct);

        return ok
            ? Result<bool>.Success(true, "درخواست ضمانت تایید شد.")
            : Result<bool>.Failure("تایید درخواست ضمانت با شکست مواجه شد.", ErrorCodes.OperationFailed, false);
    }
}
