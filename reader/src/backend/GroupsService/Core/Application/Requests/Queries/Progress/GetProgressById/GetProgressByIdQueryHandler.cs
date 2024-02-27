using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Progress.GetProgressById;

public class GetProgressByIdQueryHandler
    (IUserBookProgressRepository _progressRepository, IMapper _mapper): IRequestHandler<GetProgressByIdQuery, Result<ProgressViewDto>>
{
    public async Task<Result<ProgressViewDto>> Handle(GetProgressByIdQuery query, CancellationToken cancellationToken)
    {
        var progress = await _progressRepository.GetByIdAsync(query.ProgressId, cancellationToken);

        if (progress is null)
        {
            return new Result<ProgressViewDto>(new NotFoundError("Progress"));
        }

        if (progress.UserId != query.RequestingUserId)
        {
            return new Result<ProgressViewDto>(new BadRequestError("You are not owner of this progress"));
        }

        return new Result<ProgressViewDto>(
            new ProgressViewDto(progress.Id,
                _mapper.Map<GroupViewDto>(progress.Group),
                _mapper.Map<UserViewDto>(progress.User),
                progress.Book.BookName, progress.Progress,
                progress.UserNotes.Count));
    }
}