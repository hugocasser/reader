using Application.Abstractions.Repositories;
using Application.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Notes.CreateNote;

public class CreateNoteRequestHandler(IBooksRepository booksRepository, INotesRepository notesRepository,
    IGroupsRepository groupsRepository, IUserBookProgressRepository userBookProgressRepository) : IRequestHandler<CreateNoteRequest>
{
    public async Task Handle(CreateNoteRequest request, CancellationToken cancellationToken)
    {
        var bookToAdd = await booksRepository.GetBookByIdAsync(request.BookId);

        if (bookToAdd is null)
        {
            throw new NotFoundException("Book not found");
        }
        
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");
        }

        if (group.Members.All(user => user.Id != request.UserId))
        {
            throw new BadRequestException("You are not a member of this group");
        }
        
        if (group.AllowedBooks.All(book => book.Id != request.BookId))
        {
            throw new BadRequestException("Book is not allowed in this group");
        }
        
        var progressByUserIdAndBookId = await userBookProgressRepository.GetProgressByUserIdAndBookIdAsync(request.UserId, request.BookId);


        if (progressByUserIdAndBookId is null)
        {
            throw new NotFoundException("You don't have started reed this book in this group");
        }
        
        var note = new Note
        {
            Id = Guid.NewGuid(),
            NotePosition = request.NotePosition,
            UserBookProgressId = progressByUserIdAndBookId.Id,
            UserBookProgress = progressByUserIdAndBookId,
            Text = request.Text
        };
            
        await notesRepository.CreateNoteAsync(note);
        await notesRepository.SaveChangesAsync();
    }
    
}