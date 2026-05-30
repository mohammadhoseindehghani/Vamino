using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Queries.GetAll;

public class GetAllQueryHandler(ILoanContractService loanContractService) : IRequestHandler<GetAllQuery, Result<List<LoanContractDto>>>
{
    public async Task<Result<List<LoanContractDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var data = await loanContractService.GetAllAsync(cancellationToken);
        return Result<List<LoanContractDto>>.Success(data);
    }
}