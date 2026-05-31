using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Delete;

public class DeleteLoanGuarantorCommandValidator : AbstractValidator<DeleteLoanGuarantorCommand>
{
    public DeleteLoanGuarantorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه ضامن نامعتبر است.");
    }
}
