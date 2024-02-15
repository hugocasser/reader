using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Progress.GetProgressById;

public class GetProgressByIdQuery
    : IRequest<Result<ProgressViewDto>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid ProgressId { get; init; }
}