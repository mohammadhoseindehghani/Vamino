using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Delete;

public record DeleteLoanGuarantorCommand(int Id) : IRequest<Result<bool>>;
