using FluentValidation;

namespace Vamino.Application.Features.User.Queries.GetUserList;

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.Filter)
            .NotNull().WithMessage("فیلتر جستجو الزامی است.");

            RuleFor(x => x.Filter.Page)
                .GreaterThan(0).WithMessage("شماره صفحه باید بزرگ‌تر از صفر باشد.");

            RuleFor(x => x.Filter.PageSize)
                .GreaterThan(0).WithMessage("تعداد آیتم در صفحه باید بزرگ‌تر از صفر باشد.")
                .LessThanOrEqualTo(200).WithMessage("تعداد آیتم در صفحه نمی‌تواند بیشتر از 200 باشد.");

            RuleFor(x => x.Filter.Search)
                .MaximumLength(200).WithMessage("عبارت جستجو نمی‌تواند بیشتر از 200 کاراکتر باشد.")
                .When(x => !string.IsNullOrWhiteSpace(x.Filter.Search));
    }
}
