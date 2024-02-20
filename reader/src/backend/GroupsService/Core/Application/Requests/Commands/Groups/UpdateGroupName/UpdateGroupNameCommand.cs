using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Commands.Groups.UpdateGroupName;

public class UpdateGroupNameCommand
    : IRequest<Result<GroupViewDto>> , IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid GroupId { get; init; }
    public string Name { get; init; }
}