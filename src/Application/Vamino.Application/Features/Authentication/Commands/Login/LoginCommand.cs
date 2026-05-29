using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.Identity;

namespace Vamino.Application.Features.Authentication.Commands.Login;

public record LoginCommand(LoginRequestDto LoginDto) : IRequest<OperationResultDto>;