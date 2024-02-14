using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.AddBookToGroup;

public record AddBookToGroupCommand(Guid GroupId, Guid BookId, Guid RequestingUserId) : IRequest<Result<string>>;