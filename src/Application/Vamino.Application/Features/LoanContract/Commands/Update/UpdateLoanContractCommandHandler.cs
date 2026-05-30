using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanContract.Commands.Update;

public class UpdateLoanContractCommandHandler(ILoanContractService loanContractService)
    : IRequestHandler<UpdateLoanContractCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateLoanContractCommand request, CancellationToken ct)
    {
        var editable = await loanContractService.IsEditableAsync(request.Id, ct);
        if (!editable)
            return Result<bool>.Failure("این درخواست وام قابل ویرایش نیست.");

        var ok = await loanContractService.UpdateLoanContractAsync(request.Id, request.Model, ct);
        return ok
            ? Result<bool>.Success(true, "درخواست وام با موفقیت ویرایش شد.")
            : Result<bool>.Failure("ویرایش درخواست وام با شکست مواجه شد.");
    }
}
