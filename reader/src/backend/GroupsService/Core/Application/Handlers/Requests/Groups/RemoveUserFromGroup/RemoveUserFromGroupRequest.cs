using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveUserFromGroup;

public record RemoveUserFromGroupRequest(Guid UserToDeleteId, Guid GroupId, Guid DeleterId ) : IRequest;