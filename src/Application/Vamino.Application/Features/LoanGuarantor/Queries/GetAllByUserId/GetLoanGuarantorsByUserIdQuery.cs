using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;

namespace Vamino.Application.Features.LoanGuarantor.Queries.GetAllByUserId;

public record GetLoanGuarantorsByUserIdQuery(int UserId) : IRequest<Result<List<LoanGuarantorDto>>>;
