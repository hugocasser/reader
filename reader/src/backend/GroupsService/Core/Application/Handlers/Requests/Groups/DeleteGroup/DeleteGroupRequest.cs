using MediatR;

namespace Application.Handlers.Requests.Groups.DeleteGroup;

public record DeleteGroupRequest(Guid GroupId, Guid UserId) : IRequest;