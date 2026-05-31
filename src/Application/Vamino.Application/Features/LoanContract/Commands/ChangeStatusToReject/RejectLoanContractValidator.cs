using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToReject;

public class RejectLoanContractValidator : AbstractValidator<RejectLoanContractCommand>
{
    public RejectLoanContractValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");
    }
}
