using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Queries.Progress.GetProgressById;

public record GetProgressByIdRequest(Guid ProgressId, Guid RequestingUserId) : IRequest<Result<ProgressViewDto>>;