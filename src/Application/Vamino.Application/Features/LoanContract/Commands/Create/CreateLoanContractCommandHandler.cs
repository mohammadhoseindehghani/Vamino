using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.LoanContract.Commands.Create;

public class CreateLoanContractCommandHandler (ILoanContractService loanContractService)
    : IRequestHandler<CreateLoanContractCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateLoanContractCommand request, CancellationToken cancellationToken)
    {
        var id = await loanContractService.CreateLoanContractAsync(request.CreateLoanContractDto, cancellationToken);
        return id <=0 ? Result<int>.Failure("ایجاد درخواست وام با شکست مواجه شد.") 
            : Result<int>.Success(id, "درخواست وام با موفقیت ثبت شد.");
    }
}