using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Requests.Groups.UpdateGroupName;

public record UpdateGroupNameRequest(string Name, Guid GroupId, Guid RequestingUserId) : IRequest<Result<GroupViewDto>>;