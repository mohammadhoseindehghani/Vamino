using System.ComponentModel.DataAnnotations;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class UpdateGuarantorRequestNoteViewModel
{
    [Required]
    public int Id { get; set; }

    [Display(Name = "یادداشت")]
    [MaxLength(1000, ErrorMessage = "یادداشت نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد.")]
    public string? Note { get; set; }
}