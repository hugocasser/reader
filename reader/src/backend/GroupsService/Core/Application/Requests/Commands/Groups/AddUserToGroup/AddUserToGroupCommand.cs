using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.AddUserToGroup;

public class AddUserToGroupCommand 
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid InvitedUser { get; init; }
    public Guid GroupId { get; init; }
}