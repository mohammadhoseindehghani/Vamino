using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

namespace Vamino.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler(IIdentityService identityService) : IRequestHandler<LoginCommand, OperationResultDto>
{
    public async Task<OperationResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
       return await identityService.LoginAsync(request.LoginDto, cancellationToken);
    }
}