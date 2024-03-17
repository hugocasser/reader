using Application.Abstractions;
using Application.Common;
using Application.Results;
using MediatR;

namespace Application.Requests.Commands.Groups.RemoveUserFromGroup;

public class RemoveUserFromGroupCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid UserToRemoveId { get; init; }
    public Guid GroupId { get; init; }
}