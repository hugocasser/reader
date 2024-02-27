using Application.Abstractions;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using MediatR;

namespace Application.Requests.Queries.Progress.GetProgressById;

public record GetProgressByIdQuery(Guid ProgressId)
    : IRequest<Result<ProgressViewDto>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
}