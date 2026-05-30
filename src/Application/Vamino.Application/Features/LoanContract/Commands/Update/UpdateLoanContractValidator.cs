using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Commands.Update;

public class UpdateLoanContractValidator : AbstractValidator<UpdateLoanContractCommand>
{
    public UpdateLoanContractValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");

        RuleFor(x => x.Model).NotNull();

        RuleFor(x => x.Model.Title)
            .NotEmpty().WithMessage("عنوان الزامی است.");

        RuleFor(x => x.Model.Amount)
            .GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
    }
}
