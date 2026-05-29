using FluentValidation;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

namespace Vamino.Application.Features.User.Commands.CreateUserByAdmin;

public class CreateUserByAdminCommandValidator : AbstractValidator<CreateUserByAdminCommand>
{
    private readonly IIdentityService _identityService;

    public CreateUserByAdminCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;

        RuleFor(x => x.Dto)
            .NotNull()
            .WithMessage("اطلاعات کاربر الزامی است.");

        When(x => x.Dto is not null, () =>
        {
            RuleFor(x => x.Dto.FirstName)
                .NotEmpty().WithMessage("نام الزامی است.")
                .MaximumLength(50).WithMessage("نام حداکثر ۵۰ کاراکتر است.");

            RuleFor(x => x.Dto.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(50).WithMessage("نام خانوادگی حداکثر ۵۰ کاراکتر است.");

            RuleFor(x => x.Dto.NationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است.")
                .Matches(@"^\d{10}$").WithMessage("کد ملی باید دقیقاً ۱۰ رقم باشد.")
                .MustAsync(NationalCodeNotExists).WithMessage("این کد ملی قبلاً ثبت شده است.");

            RuleFor(x => x.Dto.PhoneNumber)
                .NotEmpty().WithMessage("شماره موبایل الزامی است.")
                .Matches(@"^09\d{9}$").WithMessage("شماره موبایل معتبر نیست. (مثال: 09123456789)")
                .MustAsync(PhoneNotExists).WithMessage("این شماره موبایل قبلاً ثبت شده است.");

            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("ایمیل الزامی است.")
                .EmailAddress().WithMessage("ایمیل معتبر نیست.")
                .MaximumLength(256).WithMessage("ایمیل حداکثر ۲۵۶ کاراکتر است.")
                .MustAsync(EmailNotExists).WithMessage("این ایمیل قبلاً ثبت شده است.");

            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage("رمز عبور الزامی است.")
                .MinimumLength(6).WithMessage("رمز عبور حداقل ۶ کاراکتر است.")
                .Matches(@"[A-Z]").WithMessage("رمز عبور باید حداقل شامل یک حرف بزرگ انگلیسی باشد.")
                .Matches(@"[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک (a-z) داشته باشد.")
                .Matches(@"[0-9]").WithMessage("رمز عبور باید حداقل یک رقم (0-9) داشته باشد.");

            RuleFor(x => x.Dto.RoleName)
                .NotEmpty().WithMessage("نقش (Role) الزامی است.")
                .MaximumLength(50).WithMessage("نام نقش حداکثر ۵۰ کاراکتر است.")
                .Must(r => AllowedRoles.Contains(r))
                .WithMessage($"نقش وارد شده معتبر نیست. نقش‌های مجاز: {string.Join(", ", AllowedRoles)}");
        });
    }

    private static readonly string[] AllowedRoles =
    {
        "Admin",
        "Customer",
        "Bank"
    };

    private Task<bool> EmailNotExists(CreateUserByAdminCommand command, string email, CancellationToken ct)
        => NotExistsAsync(() => _identityService.EmailExistsAsync(email, ct));

    private Task<bool> PhoneNotExists(CreateUserByAdminCommand command, string phone, CancellationToken ct)
        => NotExistsAsync(() => _identityService.PhoneExistsAsync(phone, ct));

    private Task<bool> NationalCodeNotExists(CreateUserByAdminCommand command, string nationalCode, CancellationToken ct)
        => NotExistsAsync(() => _identityService.NationalCodeExistsAsync(nationalCode, ct));

    private static async Task<bool> NotExistsAsync(Func<Task<bool>> existsCall)
    {
        var exists = await existsCall();
        return !exists;
    }
}