using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetById;

public record GetLoanGuarantorByIdQuery(int Id) : IRequest<Result<LoanGuarantorDto>>;
