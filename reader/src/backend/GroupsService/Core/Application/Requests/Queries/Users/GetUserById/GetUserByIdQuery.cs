using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using MediatR;

namespace Application.Requests.Queries.Users.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserViewDto>>;