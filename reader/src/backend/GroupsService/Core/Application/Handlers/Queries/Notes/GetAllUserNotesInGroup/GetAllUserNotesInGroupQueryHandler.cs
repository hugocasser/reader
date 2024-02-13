using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Handlers.Queries.Notes.GetAllUserNotes;
using Domain.Models;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllUserNotesInGroup;

public class GetAllUserNotesInGroupQueryHandler(IUserBookProgressRepository _userBookProgressRepository, IMapper _mapper)
    : IRequestHandler<GetAllUserNotesInGroupQuery, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllUserNotesInGroupQuery query, CancellationToken cancellationToken)
    {
        var userProgresses = await _userBookProgressRepository
            .GetProgressesByUserIdAndGroupIdAsync(query.RequestingUserId, query.GroupId, cancellationToken);

        if (query.RequestingUserId != query.UserId)
        {
            if (userProgresses.First().Group.AdminId != query.RequestingUserId)
            {
                return new Result<IEnumerable<NoteViewDto>>(new Error("You can't get strangers notes ", 400));
            }
        }

        var userNotes = new List<NoteViewDto>();
        
        foreach (var progress in userProgresses)
        {
            userNotes.AddRange(progress.UserNotes.Select(_mapper.Map<NoteViewDto>));
        }
        
        return new Result<IEnumerable<NoteViewDto>>(userNotes);
    }
}