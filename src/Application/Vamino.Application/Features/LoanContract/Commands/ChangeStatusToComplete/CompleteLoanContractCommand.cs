using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToComplete;

public record CompleteLoanContractCommand(int Id) : IRequest<Result<bool>>;
