using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveUserFromGroup;

public record RemoveUserFromGroupRequest(Guid UserToRemoveId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;