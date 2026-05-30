using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Reject;

public class RejectLoanGuarantorCommandValidator : AbstractValidator<RejectLoanGuarantorCommand>
{
    public RejectLoanGuarantorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه ضامن نامعتبر است.");
    }
}
