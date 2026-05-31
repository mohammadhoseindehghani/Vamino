using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAllByUserId;

public class GetLoanGuarantorsByUserIdQueryHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<GetLoanGuarantorsByUserIdQuery, Result<List<LoanGuarantorDto>>>
{
    public async Task<Result<List<LoanGuarantorDto>>> Handle(GetLoanGuarantorsByUserIdQuery request, CancellationToken ct)
    {
        var list = await loanGuarantorService.GetAllByUserIdAsync(request.UserId, ct);
        return Result<List<LoanGuarantorDto>>.Success(list);
    }
}
