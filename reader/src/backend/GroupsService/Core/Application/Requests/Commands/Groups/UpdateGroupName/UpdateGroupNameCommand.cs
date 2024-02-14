using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Commands.Groups.UpdateGroupName;

public record UpdateGroupNameCommand(string? Name, Guid GroupId, Guid RequestingUserId) : IRequest<Result<GroupViewDto>>;