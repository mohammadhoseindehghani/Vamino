using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAll;

public record GetAllLoanGuarantorsQuery() : IRequest<Result<List<LoanGuarantorDto>>>;
