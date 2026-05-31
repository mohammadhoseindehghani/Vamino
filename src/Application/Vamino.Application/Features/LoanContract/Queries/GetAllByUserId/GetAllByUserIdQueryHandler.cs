using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Queries.GetAllByUserId;

public class GetAllByUserIdQueryHandler(ILoanContractService loanContractService) 
    : IRequestHandler<GetAllByUserIdQuery, Result<List<LoanContractDto>>>
{
    public async Task<Result<List<LoanContractDto>>> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
    {
        var loanContract = await loanContractService.GetAllByUserIdAsync(request.UserId, cancellationToken);
        return Result<List<LoanContractDto>>.Success(loanContract);
    }
}