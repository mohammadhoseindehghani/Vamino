using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Models;

public class AddLoanGuarantorViewModel
{
    public int LoanContractId { get; set; }

    public string? ContractTitle { get; set; }
    public decimal? ContractAmount { get; set; }

    public string? NationalCode { get; set; }
    public string? PhoneNumber { get; set; }

    public UserSearchResultDto? FoundUser { get; set; }

    public string? ErrorMessage { get; set; }
    public string? SuccessMessage { get; set; }
}