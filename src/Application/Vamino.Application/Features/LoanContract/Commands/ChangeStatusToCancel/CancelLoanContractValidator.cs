using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToCancel;

public class CancelLoanContractValidator : AbstractValidator<CancelLoanContractCommand>
{
    public CancelLoanContractValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");
    }
}
