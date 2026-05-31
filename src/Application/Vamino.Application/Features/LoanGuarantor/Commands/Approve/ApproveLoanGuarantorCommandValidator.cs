using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Approve;

public class ApproveLoanGuarantorCommandValidator : AbstractValidator<ApproveLoanGuarantorCommand>
{
    public ApproveLoanGuarantorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه ضامن نامعتبر است.");
    }
}
