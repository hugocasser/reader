using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Handlers.Queries.Notes.GetAllUserNotes;
using Domain.Models;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllUserNotesInGroup;

public class GetAllUserNotesInGroupRequestHandler(IUserBookProgressRepository _userBookProgressRepository, IMapper _mapper)
    : IRequestHandler<GetAllUserNotesInGroupRequest, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllUserNotesInGroupRequest request, CancellationToken cancellationToken)
    {
        var userProgresses = await _userBookProgressRepository
            .GetProgressesByUserIdAndGroupIdAsync(request.RequestingUserId, request.GroupId);

        if (request.RequestingUserId != request.UserId)
        {
            if (userProgresses.First().Group.AdminId != request.RequestingUserId)
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