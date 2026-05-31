using MediatR;
using Vamino.Application.Contracts.Contracts.ProviderServices.Identity;

namespace Vamino.Application.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler(IIdentityService identityService) : IRequestHandler<LogoutCommand,Unit>
{
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await identityService.LogoutAsync();
        return Unit.Value;
    }
}