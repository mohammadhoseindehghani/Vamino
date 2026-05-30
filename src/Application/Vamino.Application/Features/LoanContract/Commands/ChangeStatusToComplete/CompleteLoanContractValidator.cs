using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToComplete;

public class CompleteLoanContractValidator : AbstractValidator<CompleteLoanContractCommand>
{
    public CompleteLoanContractValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");
    }
}
