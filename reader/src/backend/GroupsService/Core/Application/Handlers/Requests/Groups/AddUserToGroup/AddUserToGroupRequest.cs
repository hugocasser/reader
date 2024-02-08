using MediatR;

namespace Application.Handlers.Requests.Groups.AddUserToGroup;

public record AddUserToGroupRequest(Guid InvitedUser, Guid GroupId, Guid InviterId ) : IRequest;