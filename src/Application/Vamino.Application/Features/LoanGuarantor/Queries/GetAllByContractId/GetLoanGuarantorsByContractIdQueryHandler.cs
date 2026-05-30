using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAllByContractId;

public class GetLoanGuarantorsByContractIdQueryHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<GetLoanGuarantorsByContractIdQuery, Result<List<LoanGuarantorDto>>>
{
    public async Task<Result<List<LoanGuarantorDto>>> Handle(GetLoanGuarantorsByContractIdQuery request, CancellationToken ct)
    {
        var list = await loanGuarantorService.GetAllByContractIdAsync(request.ContractId, ct);
        return Result<List<LoanGuarantorDto>>.Success(list);
    }
}
