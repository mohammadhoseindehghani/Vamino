using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Queries.GetAll;

public record GetAllQuery() : IRequest<Result<List<LoanContractDto>>>;