using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAllByContractId;

public record GetLoanGuarantorsByContractIdQuery(int ContractId) : IRequest<Result<List<LoanGuarantorDto>>>;
