using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.RemoveBookFromGroup;

public record RemoveBookFromGroupCommand(Guid BookId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;