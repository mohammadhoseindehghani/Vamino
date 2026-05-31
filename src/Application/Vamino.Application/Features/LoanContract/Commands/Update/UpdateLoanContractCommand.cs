using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Commands.Update;

public record UpdateLoanContractCommand(int Id, UpdateLoanContractDto Model)
    : IRequest<Result<bool>>;
