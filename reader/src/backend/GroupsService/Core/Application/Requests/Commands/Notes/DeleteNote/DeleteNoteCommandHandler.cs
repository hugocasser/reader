using Application.Abstractions.Repositories;
using Application.Common;
using Domain.DomainEvents.Notes;
using MediatR;

namespace Application.Requests.Commands.Notes.DeleteNote;

public class DeleteNoteCommandHandler(INotesRepository notesRepository)
    : IRequestHandler<DeleteNoteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetByIdAsync(command.NoteId, cancellationToken);

        if (note is null)
        {
            return new Result<string>(new Error("Note not found", 404));
        }

        if (note.UserBookProgress.UserId != command.RequestingUserId)
        {
            return new Result<string>(new Error("You can't delete this note", 400));
        }
        
        note.Delete(new NoteDeletedEvent(note.Id));
        await notesRepository.DeleteByIdAsync(command.NoteId, cancellationToken);
        await notesRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Note deleted");
    }
}