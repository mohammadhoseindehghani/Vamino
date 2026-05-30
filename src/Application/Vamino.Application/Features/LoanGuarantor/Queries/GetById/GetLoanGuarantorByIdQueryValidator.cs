using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetById;

public class GetLoanGuarantorByIdQueryValidator : AbstractValidator<GetLoanGuarantorByIdQuery>
{
    public GetLoanGuarantorByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه ضامن نامعتبر است.");
    }
}