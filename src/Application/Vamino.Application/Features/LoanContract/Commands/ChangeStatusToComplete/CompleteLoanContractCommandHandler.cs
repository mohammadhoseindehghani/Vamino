using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToComplete;

public class CompleteLoanContractCommandHandler(ILoanContractService loanContractService)
    : IRequestHandler<CompleteLoanContractCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CompleteLoanContractCommand request, CancellationToken ct)
    {
        var (amount, approvedCount) =
            await loanContractService.GetContractSummaryForCompletionAsync(request.Id, ct);

        if (amount <= 0)
            return Result<bool>.Failure("قرارداد یافت نشد یا مبلغ نامعتبر است.");

        var requiredGuarantors = CalculateRequiredGuarantors(amount);

        if (approvedCount < requiredGuarantors)
        {
            return Result<bool>.Failure(
                $"برای تکمیل قرارداد حداقل {requiredGuarantors} ضامن تایید شده لازم است. (تایید شده: {approvedCount})");
        }

        var ok = await loanContractService.ChangeStatusToCompletedAsync(request.Id, ct);
        return ok
            ? Result<bool>.Success(true, "قرارداد با موفقیت تکمیل شد.")
            : Result<bool>.Failure("تکمیل قرارداد با شکست مواجه شد.");
    }

    private static int CalculateRequiredGuarantors(decimal amount)
    {
        const decimal step = 50_000_000m;
        return (int)Math.Ceiling(amount / step);
    }
}
