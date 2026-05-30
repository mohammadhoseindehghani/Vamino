using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Commands.Create;

public record CreateLoanGuarantorCommand(CreateLoanGuarantorDto Model)
    : IRequest<Result<int>>;
