using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Queries.GetLoanContract;

public record GetLoanContractQuery(int Id) : IRequest<Result<LoanContractDto>>;