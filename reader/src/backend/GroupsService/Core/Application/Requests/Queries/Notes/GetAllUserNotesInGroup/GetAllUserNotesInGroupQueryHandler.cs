using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using Domain.Models;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllUserNotesInGroup;

public class GetAllUserNotesInGroupQueryHandler
    (IUserBookProgressRepository _userBookProgressRepository,
        IMapper _mapper)
    : IRequestHandler<GetAllUserNotesInGroupQuery, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllUserNotesInGroupQuery query, CancellationToken cancellationToken)
    {
        var userProgresses = await _userBookProgressRepository
            .GetByAsync(progress => progress.UserId == query.RequestingUserId 
                && progress.GroupId == query.GroupId ,cancellationToken);

        if (query.RequestingUserId != query.UserId)
        {
            if (userProgresses.First().Group.AdminId != query.RequestingUserId)
            {
                return new Result<IEnumerable<NoteViewDto>>(new BadRequestError("You can't see other users notes"));
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