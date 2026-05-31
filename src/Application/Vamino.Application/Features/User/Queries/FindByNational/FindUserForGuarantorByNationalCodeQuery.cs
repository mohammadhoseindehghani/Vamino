using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.FindByNational;

public record FindUserForGuarantorByNationalCodeQuery(string NationalCode) : IRequest<Result<UserSearchResultDto>>;
