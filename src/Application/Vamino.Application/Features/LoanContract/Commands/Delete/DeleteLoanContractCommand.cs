using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanContract.Commands.Delete;

public record DeleteLoanContractCommand(int Id) : IRequest<Result<bool>>;
