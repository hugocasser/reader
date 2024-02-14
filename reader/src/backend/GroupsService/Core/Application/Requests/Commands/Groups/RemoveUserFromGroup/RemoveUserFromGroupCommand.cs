using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.RemoveUserFromGroup;

public record RemoveUserFromGroupCommand(Guid UserToRemoveId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;