using Vamino.Domain.LoanContractAgg.Enums;

namespace Vamino.Application.Contracts.Utilities.Extensions;

public static class LoanStatusExtensions
{
    public static string ToFaText(this LoanStatus status) => status switch
    {
        LoanStatus.PendingForGuarantors => "در انتظار ضامن",
        LoanStatus.PendingForBankReview => "در انتظار بررسی بانک",
        LoanStatus.Completed => "تکمیل شده",
        LoanStatus.Rejected => "رد شده",
        LoanStatus.Cancelled => "لغو شده",
        _ => "نامشخص"
    };
}