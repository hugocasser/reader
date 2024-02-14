using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Commands.Groups.CreateGroup;

public record CreateGroupCommand(string GroupName, Guid RequestingUserId) : IRequest<Result<GroupViewDto>>;