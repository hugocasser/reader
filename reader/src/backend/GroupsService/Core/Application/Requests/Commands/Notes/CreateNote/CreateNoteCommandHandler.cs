using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
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
            return new Result<NoteViewDto>(new NotFoundError("Book"));
        }
        
        var group = await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<NoteViewDto>(new NotFoundError("Group"));
        }

        if (group.Members.All(user => user.Id != command.RequestingUserId))
        {
            return new Result<NoteViewDto>(new BadRequestError("You aren't member of this group"));
        }
        
        if (group.AllowedBooks.All(book => book.Id != command.BookId))
        {
            return new Result<NoteViewDto>(new BadRequestError("Book is not allowed in this group"));
        }
        
        var progresses = await _userBookProgressRepository
            .GetByAsync(pr => pr.UserId == command.RequestingUserId
                && pr.BookId == command.BookId && pr.GroupId == command.GroupId, cancellationToken);
        
        var progress = progresses.FirstOrDefault();
        
        if (progress is null)
        {
            return new Result<NoteViewDto>(new NotFoundError("Progress"));
        }

        var note = new Note();
        note.CreateNote(command.NotePosition, progress, command.Text);
        
        await _notesRepository.CreateAsync(note, cancellationToken);
        await _notesRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<NoteViewDto>(_mapper.Map<NoteViewDto>(note));
    }
}