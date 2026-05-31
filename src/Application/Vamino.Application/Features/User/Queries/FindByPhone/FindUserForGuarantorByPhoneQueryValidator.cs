using FluentValidation;

namespace Vamino.Application.Features.User.Queries.FindByPhone;

public class FindUserForGuarantorByPhoneQueryValidator : AbstractValidator<FindUserForGuarantorByPhoneQuery>
{
    public FindUserForGuarantorByPhoneQueryValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("شماره موبایل الزامی است.")
            .MaximumLength(11).WithMessage("شماره موبایل نامعتبر است.");
    }
}