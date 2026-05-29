using MediatR;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.Identity;

namespace Vamino.Application.Features.User.Commands.CreateUserByAdmin;

public record CreateUserByAdminCommand(AdminCreateUserRequestDto Dto): IRequest<OperationResultDto>;