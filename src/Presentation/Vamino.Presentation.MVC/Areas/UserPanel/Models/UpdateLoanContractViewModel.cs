using System.ComponentModel.DataAnnotations;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class UpdateLoanContractViewModel
{
    public int Id { get; set; }

    [Display(Name = "عنوان قرارداد")]
    [Required(ErrorMessage = "عنوان ضروری است.")]
    [MinLength(5, ErrorMessage = "حداقل طول عنوان باید 5 کاراکتر باشد.")]
    public string Title { get; set; }

    [Display(Name = "مبلغ وام")]
    [Required(ErrorMessage = "مبلغ ضروری است.")]
    [Range(1, double.MaxValue, ErrorMessage = "مبلغ باید بیشتر از صفر باشد.")]
    public decimal Amount { get; set; }

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
}