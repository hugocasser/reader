using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Notes.DeleteNote;

public class DeleteNoteRequestHandler(INotesRepository notesRepository, IDbSyncerService _dbSyncerService)
    : IRequestHandler<DeleteNoteRequest, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetByIdAsync(request.NoteId, cancellationToken);

        if (note is null)
        {
            return new Result<string>(new Error("Note not found", 404));
        }

        if (note.UserBookProgress.UserId != request.RequestingUserId)
        {
            return new Result<string>(new Error("You can't delete this note", 400));
        }
        
        await notesRepository.DeleteByIdAsync(request.NoteId, cancellationToken);
        await _dbSyncerService.SendEventAsync(EventType.Deleted, note, cancellationToken);
        await notesRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Note deleted");
    }
}