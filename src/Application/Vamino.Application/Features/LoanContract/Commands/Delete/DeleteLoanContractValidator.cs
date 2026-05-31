using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Commands.Delete;

public class DeleteLoanContractValidator : AbstractValidator<DeleteLoanContractCommand>
{
    public DeleteLoanContractValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");
    }
}
