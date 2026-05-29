using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.Identity;

namespace Vamino.Application.Features.User.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserRequestDto RegisterDto) : IRequest<OperationResultDto>;