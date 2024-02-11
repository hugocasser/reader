using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.DeleteGroup;

public record DeleteGroupRequest(Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;