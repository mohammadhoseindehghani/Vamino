using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAll;

public class GetAllLoanGuarantorsQueryHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<GetAllLoanGuarantorsQuery, Result<List<LoanGuarantorDto>>>
{
    public async Task<Result<List<LoanGuarantorDto>>> Handle(GetAllLoanGuarantorsQuery request, CancellationToken ct)
    {
        var list = await loanGuarantorService.GetAllAsync(ct);
        return Result<List<LoanGuarantorDto>>.Success(list);
    }
}
