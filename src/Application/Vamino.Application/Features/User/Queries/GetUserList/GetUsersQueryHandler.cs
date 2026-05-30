using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.GetUserList;

public class GetUsersQueryHandler(IUserService userService)
    : IRequestHandler<GetUsersQuery, Result<PagedResultDto<UserListItemDto>>>
{
    public async Task<Result<PagedResultDto<UserListItemDto>>> Handle(GetUsersQuery request, CancellationToken ct)
    {
        var result = await userService.GetUsersAsync(request.Filter, ct);
        return Result<PagedResultDto<UserListItemDto>>.Success(result);
    }
}
