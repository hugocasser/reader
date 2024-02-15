using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Groups.GetGroupById;

public record GetGroupByIdQuery (Guid GroupId) : IRequest<Result<GroupViewDto>>;