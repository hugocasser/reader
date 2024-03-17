using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using MediatR;

namespace Application.Requests.Queries.Groups.GetGroupById;

public record GetGroupByIdQuery (Guid GroupId) : IRequest<Result<GroupViewDto>>;