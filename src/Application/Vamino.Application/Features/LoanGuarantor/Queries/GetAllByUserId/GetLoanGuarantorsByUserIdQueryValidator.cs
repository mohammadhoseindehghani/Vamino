using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAllByUserId;

public class GetLoanGuarantorsByUserIdQueryValidator : AbstractValidator<GetLoanGuarantorsByUserIdQuery>
{
    public GetLoanGuarantorsByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("شناسه کاربر نامعتبر است.");
    }
}