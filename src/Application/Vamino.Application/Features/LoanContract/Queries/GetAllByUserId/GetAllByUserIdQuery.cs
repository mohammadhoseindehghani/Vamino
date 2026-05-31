using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Queries.GetAllByUserId;

public record GetAllByUserIdQuery(int UserId) : IRequest<Result<List<LoanContractDto>>>;