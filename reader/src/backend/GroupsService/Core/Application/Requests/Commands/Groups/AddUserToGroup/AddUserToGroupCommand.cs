using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.AddUserToGroup;

public record AddUserToGroupCommand(Guid InvitedUser, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;