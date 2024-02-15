using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.Notes.CreateNote;

public class CreateNoteCommandHandler(IBooksRepository booksRepository, INotesRepository notesRepository,
    IGroupsRepository groupsRepository, IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<CreateNoteCommand, Result<NoteViewDto>>
{
    public async Task<Result<NoteViewDto>> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var bookToAdd = await booksRepository.GetByIdAsync(command.BookId, cancellationToken);

        if (bookToAdd is null)
        {
            return new Result<NoteViewDto>(new Error("Book not found", 404));
        }
        
        var group = await groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<NoteViewDto>(new Error("Group not found", 404));
        }

        if (group.Members.All(user => user.Id != command.RequestingUserId))
        {
            return new Result<NoteViewDto>(new Error("User not member of this group", 404));
        }
        
        if (group.AllowedBooks.All(book => book.Id != command.BookId))
        {
            return new Result<NoteViewDto>(new Error("Book isn't allowed in this group", 404));
        }
        
        var progress = await userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(command.RequestingUserId ?? Guid.Empty, command.BookId, command.GroupId, cancellationToken);


        if (progress is null)
        {
            return new Result<NoteViewDto>(new Error("Progress not found", 404));
        }

        var note = new Note();
        
        note.CreateNote(command.NotePosition, progress, command.Text);
        
        await notesRepository.CreateAsync(note, cancellationToken);
        await notesRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<NoteViewDto>(new NoteViewDto
            (note.Text, note.NotePosition, note.UserBookProgress.User.FirstName, note.UserBookProgress.User.LastName));
    }
    
}