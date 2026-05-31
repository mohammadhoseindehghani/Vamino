using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Approve;

public record ApproveLoanGuarantorCommand(int Id) : IRequest<Result<bool>>;
