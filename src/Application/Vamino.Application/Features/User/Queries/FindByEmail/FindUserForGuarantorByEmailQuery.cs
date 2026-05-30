using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.FindByEmail;

public record FindUserForGuarantorByEmailQuery(string Email) : IRequest<Result<UserSearchResultDto>>;
