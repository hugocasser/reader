using Application.Abstractions;
using Application.Common;
using Application.Results;
using MediatR;

namespace Application.Requests.Commands.Groups.AddBookToGroup;

public class AddBookToGroupCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid GroupId { get; init; }
    public Guid BookId { get; init; }
}