using MediatR;

namespace Application.Handlers.Requests.Groups.UpdateGroupName;

public record UpdateGroupNameRequest(string Name, Guid GroupId, Guid UserId) : IRequest;