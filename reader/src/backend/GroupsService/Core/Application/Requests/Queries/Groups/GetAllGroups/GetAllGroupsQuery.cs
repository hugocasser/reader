using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Groups.GetAllGroups;

public record GetAllGroupsQuery(PageSettingsRequestDto PageSettingsRequestDto) : IRequest<Result<IEnumerable<GroupViewDto>>>;