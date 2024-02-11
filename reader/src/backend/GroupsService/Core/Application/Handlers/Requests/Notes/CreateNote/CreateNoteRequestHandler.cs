using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Notes.CreateNote;

public class CreateNoteRequestHandler(IBooksRepository booksRepository, INotesRepository notesRepository,
    IGroupsRepository groupsRepository, IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<CreateNoteRequest, Result<NoteViewDto>>
{
    public async Task<Result<NoteViewDto>> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
    {
        var bookToAdd = await booksRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (bookToAdd is null)
        {
            return new Result<NoteViewDto>(new Error("Book not found", 404));
        }
        
        var group = await groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<NoteViewDto>(new Error("Group not found", 404));
        }

        if (group.Members.All(user => user.Id != request.RequestingUserId))
        {
            return new Result<NoteViewDto>(new Error("User not member of this group", 404));
        }
        
        if (group.AllowedBooks.All(book => book.Id != request.BookId))
        {
            return new Result<NoteViewDto>(new Error("Book isn't allowed in this group", 404));
        }
        
        var progressByUserIdAndBookId = await userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(request.RequestingUserId, request.BookId, request.GroupId);


        if (progressByUserIdAndBookId is null)
        {
            return new Result<NoteViewDto>(new Error("Progress not found", 404));
        }
        
        var note = new Note
        {
            Id = Guid.NewGuid(),
            NotePosition = request.NotePosition,
            UserBookProgressId = progressByUserIdAndBookId.Id,
            UserBookProgress = progressByUserIdAndBookId,
            Text = request.Text
        };
            
        await notesRepository.CreateAsync(note, cancellationToken);
        await notesRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<NoteViewDto>(new NoteViewDto
            (note.Text, note.NotePosition, note.UserBookProgress.User.FirstName, note.UserBookProgress.User.LastName));
    }
    
}