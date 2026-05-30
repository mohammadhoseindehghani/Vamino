using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetById;

public class GetLoanGuarantorByIdQueryHandler(ILoanGuarantorService loanGuarantorService)
    : IRequestHandler<GetLoanGuarantorByIdQuery, Result<LoanGuarantorDto>>
{
    public async Task<Result<LoanGuarantorDto>> Handle(GetLoanGuarantorByIdQuery request, CancellationToken ct)
    {
        var dto = await loanGuarantorService.GetByIdAsync(request.Id, ct);
        return dto is null
            ? Result<LoanGuarantorDto>.Failure("ضامن یافت نشد.", ErrorCodes.NotFound)
            : Result<LoanGuarantorDto>.Success(dto);
    }
}
