using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.AddBookToGroup;

public record AddBookToGroupRequest(Guid GroupId, Guid BookId, Guid RequestingUserId) : IRequest<Result<string>>;