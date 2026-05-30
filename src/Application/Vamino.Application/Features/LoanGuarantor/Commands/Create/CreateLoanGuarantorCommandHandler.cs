using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Create;

public class CreateLoanGuarantorCommandHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<CreateLoanGuarantorCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateLoanGuarantorCommand request, CancellationToken ct)
    {
        var canBe = await loanGuarantorService.CanBeGuarantorForContractAsync(
            request.Model.LoanContractId, request.Model.UserId, ct);

        if (!canBe)
            return Result<int>.Failure("این کاربر نمی‌تواند ضامن این قرارداد باشد.", ErrorCodes.Conflict);

        var id = await loanGuarantorService.CreateAsync(request.Model, ct);
        return id <= 0
            ? Result<int>.Failure("ایجاد ضامن با شکست مواجه شد.", ErrorCodes.OperationFailed)
            : Result<int>.Success(id, "ضامن با موفقیت اضافه شد.");
    }
}
