using FluentValidation;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAllByContractId;

public class GetLoanGuarantorsByContractIdQueryValidator : AbstractValidator<GetLoanGuarantorsByContractIdQuery>
{
    public GetLoanGuarantorsByContractIdQueryValidator()
    {
        RuleFor(x => x.ContractId)
            .GreaterThan(0).WithMessage("شناسه قرارداد نامعتبر است.");
    }
}