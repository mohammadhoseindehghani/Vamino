using FluentValidation;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

namespace Vamino.Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IIdentityService identityService)
    {
        RuleFor(x => x.RegisterDto.FirstName)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(100).WithMessage("نام نمی‌تواند بیشتر از 100 کاراکتر باشد.");

        RuleFor(x => x.RegisterDto.LastName)
            .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
            .MaximumLength(100).WithMessage("نام خانوادگی نمی‌تواند بیشتر از 100 کاراکتر باشد.");

        RuleFor(x => x.RegisterDto.Email)
            .NotEmpty().WithMessage("ایمیل الزامی است.")
            .EmailAddress().WithMessage("فرمت ایمیل معتبر نیست.")
            .MustAsync(async (email, cancellationToken) =>
                !await identityService.EmailExistsAsync(email, cancellationToken))
            .WithMessage("این ایمیل قبلاً ثبت شده است.");

        RuleFor(x => x.RegisterDto.PhoneNumber)
            .NotEmpty().WithMessage("شماره موبایل الزامی است.")
            .Matches(@"^09\d{9}$").WithMessage("فرمت شماره موبایل معتبر نیست.")
            .MustAsync(async (phoneNumber, cancellationToken) =>
                !await identityService.PhoneExistsAsync(phoneNumber, cancellationToken))
            .WithMessage("این شماره موبایل قبلاً ثبت شده است.");

        RuleFor(x => x.RegisterDto.NationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است.")
            .Length(10).WithMessage("کد ملی باید 10 رقم باشد.")
            .Matches(@"^\d{10}$").WithMessage("کد ملی باید فقط شامل عدد باشد.")
            .MustAsync(async (nationalCode, cancellationToken) =>
                !await identityService.NationalCodeExistsAsync(nationalCode, cancellationToken))
            .WithMessage("این کد ملی قبلاً ثبت شده است.");

        RuleFor(x => x.RegisterDto.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است.")
            .MinimumLength(6).WithMessage("رمز عبور باید حداقل 6 کاراکتر باشد.")
            .MaximumLength(100).WithMessage("رمز عبور نمی‌تواند بیشتر از 100 کاراکتر باشد.")
            .Matches(@"[A-Z]").WithMessage("رمز عبور باید حداقل شامل یک حرف بزرگ انگلیسی باشد.")
            .Matches(@"[a-z]").WithMessage("رمز عبور باید حداقل شامل یک حرف کوچک انگلیسی باشد.")
            .Matches(@"\d").WithMessage("رمز عبور باید حداقل شامل یک عدد باشد.");

        RuleFor(x => x.RegisterDto.ConfirmPassword)
            .NotEmpty().WithMessage("تکرار رمز عبور الزامی است.")
            .Equal(x => x.RegisterDto.Password).WithMessage("رمز عبور و تکرار آن یکسان نیستند.");
    }

    private static bool IsValidNationalCode(string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode))
            return false;

        if (nationalCode.Length != 10 || !nationalCode.All(char.IsDigit))
            return false;

        var invalidCodes = new[]
        {
            "0000000000", "1111111111", "2222222222", "3333333333", "4444444444",
            "5555555555", "6666666666", "7777777777", "8888888888", "9999999999"
        };

        if (invalidCodes.Contains(nationalCode))
            return false;

        var check = int.Parse(nationalCode[9].ToString());
        var sum = Enumerable.Range(0, 9)
            .Select(x => int.Parse(nationalCode[x].ToString()) * (10 - x))
            .Sum();

        var remainder = sum % 11;

        return remainder < 2
            ? check == remainder
            : check == 11 - remainder;
    }
}