using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Delete;

public class DeleteLoanGuarantorCommandHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<DeleteLoanGuarantorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteLoanGuarantorCommand request, CancellationToken ct)
    {
        var ok = await loanGuarantorService.DeleteAsync(request.Id, ct);

        return ok
            ? Result<bool>.Success(true, "حذف ضامن با موفقیت انجام شد.")
            : Result<bool>.Failure("حذف ضامن با شکست مواجه شد.", ErrorCodes.OperationFailed, false);
    }
}
