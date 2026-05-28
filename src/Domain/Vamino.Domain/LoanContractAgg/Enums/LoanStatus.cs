namespace Vamino.Domain.LoanContractAgg.Enums;

public enum LoanStatus
{
    PendingForBankReview = 1,
    PendingForGuarantors = 2,
    Completed = 3,
    Rejected = 4,
    Cancelled = 5
}