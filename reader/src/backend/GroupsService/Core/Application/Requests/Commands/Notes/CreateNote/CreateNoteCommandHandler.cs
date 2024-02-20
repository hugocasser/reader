using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Views;
using Domain.Models;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Commands.Notes.CreateNote;

public class CreateNoteCommandHandler(IBooksRepository _booksRepository, INotesRepository _notesRepository,
    IGroupsRepository _groupsRepository, IUserBookProgressRepository _userBookProgressRepository, IMapper _mapper)
    : IRequestHandler<CreateNoteCommand, Result<NoteViewDto>>
{
    public async Task<Result<NoteViewDto>> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var bookToAdd = await _booksRepository.GetByIdAsync(command.BookId, cancellationToken);

        if (bookToAdd is null)
        {
            return new Result<NoteViewDto>(new Error("Book not found", 404));
        }
        
        var group = await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

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
        
        var progress = await _userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(command.RequestingUserId ?? Guid.Empty, command.BookId, command.GroupId, cancellationToken);


        if (progress is null)
        {
            return new Result<NoteViewDto>(new Error("Progress not found", 404));
        }

        var note = new Note();
        note.CreateNote(command.NotePosition, progress, command.Text);
        
        await _notesRepository.CreateAsync(note, cancellationToken);
        await _notesRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<NoteViewDto>(_mapper.Map<NoteViewDto>(note));
    }
}