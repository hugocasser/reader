using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.DeleteGroup;

public record DeleteGroupCommand(Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;