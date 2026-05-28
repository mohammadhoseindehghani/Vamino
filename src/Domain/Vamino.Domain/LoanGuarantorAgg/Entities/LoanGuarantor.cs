using Vamino.Domain._Common;
using Vamino.Domain.LoanContractAgg.Entities;
using Vamino.Domain.LoanGuarantorAgg.Enums;
using Vamino.Domain.UserAgg.Entities;

namespace Vamino.Domain.LoanGuarantorAgg.Entities;

public class LoanGuarantor : BaseEntity
{
    public int LoanContractId { get; set; }
    public int UserId { get; set; }
    public GuarantorStatus GuarantorStatus { get; set; }
    public string? Note { get; set; }
    public DateTime? RespondedAt { get; set; }

    public LoanContract LoanContract { get; set; }
    public User User { get; set; } 
}