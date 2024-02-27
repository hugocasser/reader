using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllGroupBookNotes;

public class GetAllGroupBookNotesQueryHandler
    (INotesRepository _notesRepository, IMapper _mapper): IRequestHandler<GetAllGroupBookNotesQuery, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllGroupBookNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _notesRepository.GetNotesByGroupIdAndBookIdAsync(request.GroupId, request.BookId, cancellationToken);
        var note = notes.First();

        return note.UserBookProgress.Group.Members.FirstOrDefault(user => user.Id == request.RequestingUserId) == null 
            ? new Result<IEnumerable<NoteViewDto>>(new BadRequestError("You aren't member of this group")) 
            : new Result<IEnumerable<NoteViewDto>>(notes.Select(_mapper.Map<NoteViewDto>));
    }
}