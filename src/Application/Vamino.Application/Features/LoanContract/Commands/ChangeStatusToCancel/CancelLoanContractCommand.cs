using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToCancel;

public record CancelLoanContractCommand(int Id) : IRequest<Result<bool>>;
