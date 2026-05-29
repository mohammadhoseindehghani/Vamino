using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

namespace Vamino.Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommandHandler(IIdentityService identityService
) : IRequestHandler<RegisterUserCommand, OperationResultDto>
{
    public async Task<OperationResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.RegisterAsync(request.RegisterDto, cancellationToken);
    }
}