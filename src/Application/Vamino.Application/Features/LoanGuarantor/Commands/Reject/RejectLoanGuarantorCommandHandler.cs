using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Reject;

public class RejectLoanGuarantorCommandHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<RejectLoanGuarantorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RejectLoanGuarantorCommand request, CancellationToken ct)
    {
        var ok = await loanGuarantorService.RejectAsync(request.Id, ct);

        return ok
            ? Result<bool>.Success(true, "درخواست ضمانت رد شد.")
            : Result<bool>.Failure("رد کردن درخواست ضمانت با شکست مواجه شد.", ErrorCodes.OperationFailed, false);
    }
}
