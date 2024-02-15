using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Requests.Queries.Notes.GetNoteById;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetNoteById;

public class GetNoteByIdQueryHandler(INotesRepository _notesRepository, IUsersRepository _usersRepository) : IRequestHandler<GetNoteByIdQuery, Result<NoteViewDto>>
{
    public async Task<Result<NoteViewDto>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _notesRepository.GetByIdAsync(request.NoteId, cancellationToken);

        if (note is null)
        {
            return new Result<NoteViewDto>(new Error("Note not found", 404));
        }

        if (note.UserBookProgress.User.Id != request.RequestingUserId)
        {
            return new Result<NoteViewDto>(new Error("You are not the owner of this note", 400));
        }
        
        var user = await _usersRepository.GetByIdAsync(request.RequestingUserId ??Guid.Empty, cancellationToken);
        
        return new Result<NoteViewDto>(new NoteViewDto(note.Text, note.NotePosition, user.FirstName, user.LastName));
    }
}