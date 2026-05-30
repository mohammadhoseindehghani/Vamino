using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanContract;

namespace Vamino.Application.Features.LoanContract.Commands.Create;

public record CreateLoanContractCommand(CreateLoanContractDto CreateLoanContractDto) : IRequest<Result<int>>;