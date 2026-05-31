using FluentValidation;

namespace Vamino.Application.Features.User.Queries.CheckUserExistsById;

public class CheckUserExistsByIdQueryValidator : AbstractValidator<CheckUserExistsByIdQuery>
{
    public CheckUserExistsByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("شناسه کاربر نامعتبر است.");
    }
}
