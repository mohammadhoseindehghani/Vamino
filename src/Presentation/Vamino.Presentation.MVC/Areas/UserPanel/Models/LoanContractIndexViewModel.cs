using Vamino.Application.Contracts.DTOs.LoanContract;
using Vamino.Domain.LoanContractAgg.Enums;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class LoanContractIndexViewModel
{
    public List<LoanContractDto> Contracts { get; set; } = new();

    public int TotalCount => Contracts.Count;
    public int PendingGuarantorCount => Contracts.Count(c => c.LoanStatus == LoanStatus.PendingForGuarantors); 
    public int PendingBankReviewCount => Contracts.Count(c => c.LoanStatus == LoanStatus.PendingForBankReview);
    public int CompletedCount => Contracts.Count(c => c.LoanStatus == LoanStatus.Completed);
}
