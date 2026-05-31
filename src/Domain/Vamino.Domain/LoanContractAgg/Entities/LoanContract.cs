using Vamino.Domain._Common;
using Vamino.Domain.LoanContractAgg.Enums;
using Vamino.Domain.LoanGuarantorAgg.Entities;
using Vamino.Domain.UserAgg.Entities;

namespace Vamino.Domain.LoanContractAgg.Entities;

public class LoanContract : BaseEntity
{
    public string Title { get; set; }
    public int BorrowerId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public LoanStatus LoanStatus { get; set; }

    public User Borrower { get; set; }
    public ICollection<LoanGuarantor> LoanGuarantors { get; set; } = new List<LoanGuarantor>();
}