using Vamino.Domain.LoanGuarantorAgg.Enums;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class GuarantorRequestsIndexViewModel
{
    public string? Search { get; set; }

    public GuarantorStatus? Status { get; set; }

    public List<GuarantorRequestItemViewModel> Items { get; set; } = [];
}