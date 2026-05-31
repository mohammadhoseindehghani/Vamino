using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Update;

public class UpdateLoanGuarantorNoteCommandValidator : AbstractValidator<UpdateLoanGuarantorNoteCommand>
{
    public UpdateLoanGuarantorNoteCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه ضامن نامعتبر است.");

        RuleFor(x => x.Note)
            .NotNull().WithMessage("یادداشت نمی‌تواند null باشد.")
            .MaximumLength(1000).WithMessage("حداکثر طول یادداشت 1000 کاراکتر است.");
    }
}
