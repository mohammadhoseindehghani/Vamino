using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;

namespace Vamino.Application.Features.User.Queries.CheckUserExistsById;

public class CheckUserExistsByIdQueryHandler(IUserService userService)
    : IRequestHandler<CheckUserExistsByIdQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(CheckUserExistsByIdQuery request, CancellationToken ct)
    {
        var exists = await userService.ExistsByIdAsync(request.UserId, ct);

        return exists
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("کاربر یافت نشد.", ErrorCodes.NotFound, false);
    }
}
