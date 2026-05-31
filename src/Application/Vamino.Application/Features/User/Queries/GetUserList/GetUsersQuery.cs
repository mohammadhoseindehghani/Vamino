using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.GetUserList;

public record GetUsersQuery(GetUsersFilterDto Filter)
    : IRequest<Result<PagedResultDto<UserListItemDto>>>;
