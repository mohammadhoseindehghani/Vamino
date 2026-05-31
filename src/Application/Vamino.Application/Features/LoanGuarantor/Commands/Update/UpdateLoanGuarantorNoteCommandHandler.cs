using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Update;

public class UpdateLoanGuarantorNoteCommandHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<UpdateLoanGuarantorNoteCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateLoanGuarantorNoteCommand request, CancellationToken ct)
    {
        var ok = await loanGuarantorService.UpdateNoteAsync(request.Id, request.Note, ct);

        return ok
            ? Result<bool>.Success(true, "یادداشت با موفقیت ویرایش شد.")
            : Result<bool>.Failure("ویرایش یادداشت با شکست مواجه شد.", ErrorCodes.OperationFailed, false);
    }
}
