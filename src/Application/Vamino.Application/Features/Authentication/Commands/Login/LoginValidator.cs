using FluentValidation;

namespace Vamino.Application.Features.Authentication.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.LoginDto.UserNameOrEmailOrPhone)
            .NotEmpty().WithMessage("نام کاربری نمیتواند خلی باشد.")
            .MinimumLength(3).WithMessage("نام کاربری نمیتواند کمتر از 3 کاراکتر باشد.");

        RuleFor(x => x.LoginDto.Password)
            .MinimumLength(6).WithMessage("رمز عبور حداقل باید 6 کاراکتر باشد.");
    }
}