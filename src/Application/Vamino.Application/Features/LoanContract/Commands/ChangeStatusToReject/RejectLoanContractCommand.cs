using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanContract.Commands.ChangeStatusToReject;

public record RejectLoanContractCommand(int Id) : IRequest<Result<bool>>;
