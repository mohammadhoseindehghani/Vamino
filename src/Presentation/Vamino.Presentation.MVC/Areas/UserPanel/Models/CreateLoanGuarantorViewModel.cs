namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class CreateLoanGuarantorViewModel
{
    public int LoanContractId { get; set; }
    public int UserId { get; set; }
    public string? Note { get; set; }
}