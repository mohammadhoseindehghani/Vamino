using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.FindByEmail;

public class FindUserForGuarantorByEmailQueryHandler(IUserService userService)
    : IRequestHandler<FindUserForGuarantorByEmailQuery, Result<UserSearchResultDto>>
{
    public async Task<Result<UserSearchResultDto>> Handle(FindUserForGuarantorByEmailQuery request, CancellationToken ct)
    {
        var user = await userService.FindForGuarantorByEmailAsync(request.Email, ct);

        return user is null
            ? Result<UserSearchResultDto>.Failure("کاربری با این ایمیل یافت نشد.", ErrorCodes.NotFound)
            : Result<UserSearchResultDto>.Success(user);
    }
}
