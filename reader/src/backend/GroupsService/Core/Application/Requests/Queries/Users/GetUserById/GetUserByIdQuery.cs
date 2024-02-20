using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Users.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserViewDto>>;