using Application.Abstractions.Services.Cache;
using Application.Results;
using Application.Results.Errors;
using Domain.DomainEvents.Notes;
using MediatR;

namespace Application.Requests.Commands.Notes.DeleteNote;

public class DeleteNoteCommandHandler(ICashedNotesService _cashedNotesService)
    : IRequestHandler<DeleteNoteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await _cashedNotesService.GetByIdAsync(command.NoteId, cancellationToken);

        if (note is null)
        {
            return new Result<string>(new NotFoundError("Note"));
        }

        if (note.UserBookProgress.UserId != command.RequestingUserId)
        {
            return new Result<string>(new BadRequestError("You are not owner"));
        }

        note.Delete(new NoteDeletedEvent(note.Id));
        
        await _cashedNotesService.DeleteByIdAsync(command.NoteId, cancellationToken);

        return new Result<string>();
    }
}