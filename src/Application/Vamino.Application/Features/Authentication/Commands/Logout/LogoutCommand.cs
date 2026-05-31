using MediatR;

namespace Vamino.Application.Features.Authentication.Commands.Logout;

public record LogoutCommand() : IRequest<Unit>;