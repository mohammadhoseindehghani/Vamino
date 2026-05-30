using FluentValidation;

namespace Vamino.Application.Features.User.Queries.FindByEmail;

public class FindUserForGuarantorByEmailQueryValidator : AbstractValidator<FindUserForGuarantorByEmailQuery>
{
    public FindUserForGuarantorByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("ایمیل الزامی است.")
            .EmailAddress().WithMessage("فرمت ایمیل نامعتبر است.")
            .MaximumLength(256).WithMessage("ایمیل نامعتبر است.");
    }
}
