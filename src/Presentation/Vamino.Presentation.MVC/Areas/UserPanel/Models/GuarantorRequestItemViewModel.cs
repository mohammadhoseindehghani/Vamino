using Vamino.Domain.LoanGuarantorAgg.Enums;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class GuarantorRequestItemViewModel
{
    public int Id { get; set; }

    public int LoanContractId { get; set; }

    public int UserId { get; set; }

    public GuarantorStatus Status { get; set; }

    public string? Note { get; set; }

    public DateTime? RespondedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ContractTitle { get; set; } = "";

    public decimal? Amount { get; set; }

    public bool CanRespond => Status == GuarantorStatus.Pending;

    public bool CanEditNote => true;
}