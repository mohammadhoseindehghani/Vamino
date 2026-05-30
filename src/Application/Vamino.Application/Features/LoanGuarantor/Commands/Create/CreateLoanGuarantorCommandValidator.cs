namespace Vamino.Application.Features.LoanGuarantor.Commands.Create;

using FluentValidation;

public class CreateLoanGuarantorCommandValidator : AbstractValidator<CreateLoanGuarantorCommand>
{
    public CreateLoanGuarantorCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().WithMessage("اطلاعات ضامن الزامی است.");

        RuleFor(x => x.Model.LoanContractId)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");

        RuleFor(x => x.Model.UserId)
            .GreaterThan(0).WithMessage("شناسه کاربر نامعتبر است.");

        RuleFor(x => x.Model.Note)
            .MaximumLength(1000).WithMessage("حداکثر طول یادداشت 1000 کاراکتر است.");

    }
}
