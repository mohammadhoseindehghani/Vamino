using FluentValidation;

namespace Vamino.Application.Features.User.Queries.FindByNational;

public class FindUserForGuarantorByNationalCodeQueryValidator 
    : AbstractValidator<FindUserForGuarantorByNationalCodeQuery>
{
    public FindUserForGuarantorByNationalCodeQueryValidator()
    {
        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است.")
            .Length(10).WithMessage("کد ملی باید 10 رقم باشد.");
    }
}
