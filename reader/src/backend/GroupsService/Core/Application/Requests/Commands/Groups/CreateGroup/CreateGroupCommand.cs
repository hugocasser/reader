using Application.Abstractions;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using MediatR;

namespace Application.Requests.Commands.Groups.CreateGroup;

public class CreateGroupCommand
    : IRequest<Result<GroupViewDto>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }

    public string GroupName { get; init; }
}