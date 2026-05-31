using Vamino.Domain.LoanContractAgg.Enums;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class DeleteLoanContractViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public LoanStatus LoanStatus { get; set; }
}
