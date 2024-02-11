using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Requests.Groups.CreateGroup;

public record CreateGroupRequest(string GroupName, Guid RequestingUserId) : IRequest<Result<GroupViewDto>>;