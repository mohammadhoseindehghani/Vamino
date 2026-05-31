using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Queries.GetLoanContract;

public class GetLoanContractQueryHandler(ILoanContractService loanContractService) 
    : IRequestHandler<GetLoanContractQuery, Result<LoanContractDto>>
{
    public async Task<Result<LoanContractDto>> Handle(GetLoanContractQuery request, CancellationToken cancellationToken)
    {
        var loanContract = await loanContractService.GetLoanContractAsync(request.Id, cancellationToken);
        if (loanContract is null)
        {
            return Result<LoanContractDto>.Failure("هیچ قراردادی با این ایدی یافت نشد.");
        }
        return Result<LoanContractDto>.Success(loanContract);
    }
}