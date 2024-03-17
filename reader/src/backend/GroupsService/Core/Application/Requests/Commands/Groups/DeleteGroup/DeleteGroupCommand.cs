using Application.Abstractions;
using Application.Common;
using Application.Results;
using MediatR;

namespace Application.Requests.Commands.Groups.DeleteGroup;

public class DeleteGroupCommand
    : IRequest<Result<string>> , IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid GroupId { get; init; }
}