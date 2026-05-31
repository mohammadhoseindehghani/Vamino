using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Commands.Create;

public class CreateLoanContractValidator : AbstractValidator<CreateLoanContractCommand>
{
    public CreateLoanContractValidator()
    {
        RuleFor(x => x.CreateLoanContractDto.Title)
            .NotEmpty().WithMessage("عنوان الزامی است.")
            .MinimumLength(5)
            .WithMessage("حداقل طول عنوان 5 کاراکتر است.");

        RuleFor(x => x.CreateLoanContractDto.BorrowerId)
            .GreaterThan(0).WithMessage("شناسه وام‌گیرنده نامعتبر است.");

        RuleFor(x => x.CreateLoanContractDto.Amount)
            .GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
    }
}
