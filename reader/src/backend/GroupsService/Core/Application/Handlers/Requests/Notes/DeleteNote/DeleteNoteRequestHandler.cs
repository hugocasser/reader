using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Notes.DeleteNote;

public class DeleteNoteRequestHandler(INotesRepository notesRepository) : IRequestHandler<DeleteNoteRequest>
{
    public async Task Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteAsync(request.NoteId);

        if (note is null)
        {
            throw new NotFoundException("Note not found");
        }

        if (note.UserBookProgress.UserId != request.UserId)
        {
            throw new BadRequestException("You are not the owner of this note");
        }
        
        await notesRepository.DeleteNoteByIdAsync(request.NoteId);
        await notesRepository.SaveChangesAsync();
    }
}