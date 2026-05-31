using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Features.User.Queries.CheckUserExistsById;

public record CheckUserExistsByIdQuery(int UserId)
    : IRequest<Result<bool>>;
