using System.ComponentModel.DataAnnotations;

namespace Vamino.Presentation.MVC.Areas.Auth.Models;

public class RegisterViewModel
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "نام الزامی است")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "نام خانوادگی الزامی است")]
    public string LastName { get; set; }

    [Display(Name = "کد ملی")]
    [Required(ErrorMessage = "کد ملی الزامی است")]
    [MaxLength(10)]
    public string NationalCode { get; set; }

    [Display(Name = "شماره موبایل")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره موبایل باید دقیقاً ۱۱ رقم باشد")]
    [Required(ErrorMessage = "شماره موبایل الزامی است")]
    public string PhoneNumber { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "ایمیل الزامی است")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "رمز عبور الزامی است")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
    [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}