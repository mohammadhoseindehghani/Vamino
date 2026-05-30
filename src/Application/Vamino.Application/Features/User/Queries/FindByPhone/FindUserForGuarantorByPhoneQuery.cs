using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.FindByPhone;

public record FindUserForGuarantorByPhoneQuery(string PhoneNumber) : IRequest<Result<UserSearchResultDto>>;
