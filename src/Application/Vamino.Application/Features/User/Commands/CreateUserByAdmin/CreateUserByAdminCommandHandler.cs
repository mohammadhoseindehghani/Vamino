using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

namespace Vamino.Application.Features.User.Commands.CreateUserByAdmin;

public class CreateUserByAdminCommandHandler(IIdentityService identityService) 
    : IRequestHandler<CreateUserByAdminCommand,OperationResultDto>
{
    public async Task<OperationResultDto> Handle(CreateUserByAdminCommand request, CancellationToken cancellationToken)
    {
        return await identityService.AdminCreateUserAsync(request.Dto, cancellationToken);
    }
}