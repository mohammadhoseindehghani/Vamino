using System.ComponentModel.DataAnnotations;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class RejectGuarantorRequestViewModel
{
    [Required]
    public int Id { get; set; }
}