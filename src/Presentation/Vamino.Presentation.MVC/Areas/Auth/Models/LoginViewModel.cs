using System.ComponentModel.DataAnnotations;

namespace Vamino.Presentation.MVC.Areas.Auth.Models;

public class LoginViewModel
{
    [Display(Name = "ایمیل / شماره موبایل")]
    [Required(ErrorMessage = "لطفا ایمیل یا شماره موبایل را وارد کنید")]
    public string UserNameOrEmailOrPhone { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}