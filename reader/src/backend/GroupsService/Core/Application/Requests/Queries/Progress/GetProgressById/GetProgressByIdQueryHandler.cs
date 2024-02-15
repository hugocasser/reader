using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
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
            return new Result<ProgressViewDto>(new Error("Progress Not Found", 404));
        }

        if (progress.UserId != query.RequestingUserId)
        {
            return new Result<ProgressViewDto>(new Error("You are not owner of this progress", 400));
        }

        return new Result<ProgressViewDto>(
            new ProgressViewDto(progress.Id,
                _mapper.Map<GroupViewDto>(progress.Group),
                _mapper.Map<UserViewDto>(progress.User),
                progress.Book.BookName, progress.Progress,
                progress.UserNotes.Count));
    }
}