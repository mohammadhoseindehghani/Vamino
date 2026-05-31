using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.FindByPhone;

public class FindUserForGuarantorByPhoneQueryHandler(IUserService userService)
    : IRequestHandler<FindUserForGuarantorByPhoneQuery, Result<UserSearchResultDto>>
{
    public async Task<Result<UserSearchResultDto>> Handle(FindUserForGuarantorByPhoneQuery request, CancellationToken ct)
    {
        var user = await userService.FindForGuarantorByPhoneAsync(request.PhoneNumber, ct);

        return user is null
            ? Result<UserSearchResultDto>.Failure("کاربری با این شماره موبایل یافت نشد.", ErrorCodes.NotFound)
            : Result<UserSearchResultDto>.Success(user);
    }
}
