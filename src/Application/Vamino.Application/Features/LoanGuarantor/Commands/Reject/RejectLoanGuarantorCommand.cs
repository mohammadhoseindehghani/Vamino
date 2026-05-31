using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Reject;

public record RejectLoanGuarantorCommand(int Id) : IRequest<Result<bool>>;
