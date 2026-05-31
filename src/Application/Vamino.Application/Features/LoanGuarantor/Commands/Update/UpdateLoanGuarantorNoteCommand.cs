using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Update;

public record UpdateLoanGuarantorNoteCommand(int Id, string Note) : IRequest<Result<bool>>;
