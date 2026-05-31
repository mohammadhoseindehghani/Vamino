using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Queries.GetLoanContract;

public class GetLoanContractValidator : AbstractValidator<GetLoanContractQuery>
{
    public GetLoanContractValidator()
    {
        RuleFor(x=>x.Id).GreaterThan(0).WithMessage("ایدی قرارداد معتبر نمیباشد.");
    }
}