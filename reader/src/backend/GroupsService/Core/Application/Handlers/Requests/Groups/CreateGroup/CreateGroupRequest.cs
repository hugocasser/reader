using MediatR;

namespace Application.Handlers.Requests.Groups.CreateGroup;

public record CreateGroupRequest(Guid AdminId, string GroupName) : IRequest;