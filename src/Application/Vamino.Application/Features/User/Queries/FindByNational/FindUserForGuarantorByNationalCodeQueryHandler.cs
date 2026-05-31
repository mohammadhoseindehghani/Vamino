using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.DomainServices;
using Vamino.Application.Contracts.DTOs.User;

namespace Vamino.Application.Features.User.Queries.FindByNational;

public class FindUserForGuarantorByNationalCodeQueryHandler(IUserService userService)
    : IRequestHandler<FindUserForGuarantorByNationalCodeQuery, Result<UserSearchResultDto>>
{
    public async Task<Result<UserSearchResultDto>> Handle(FindUserForGuarantorByNationalCodeQuery request, CancellationToken ct)
    {
        var user = await userService.FindForGuarantorByNationalCodeAsync(request.NationalCode, ct);

        return user is null
            ? Result<UserSearchResultDto>.Failure("کاربری با این کد ملی یافت نشد.", ErrorCodes.NotFound)
            : Result<UserSearchResultDto>.Success(user);
    }
}
