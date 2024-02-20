using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.RemoveBookFromGroup;

public class RemoveBookFromGroupCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid BookId { get; init; }
    public Guid GroupId { get; init; }
}