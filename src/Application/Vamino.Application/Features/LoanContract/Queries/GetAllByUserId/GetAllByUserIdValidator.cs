using FluentValidation;

namespace Vamino.Application.Features.LoanContract.Queries.GetAllByUserId;

public class GetAllByUserIdValidator : AbstractValidator<GetAllByUserIdQuery>
{
    public GetAllByUserIdValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("ایدی کاربر درست نمیباشد.");
    }
}