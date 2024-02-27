using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
using MediatR;

namespace Application.Requests.Commands.Groups.AddBookToGroup;

public class AddBookToGroupCommandHandler(IBooksRepository _booksRepository,
    IGroupsRepository _groupsRepository)
    : IRequestHandler<AddBookToGroupCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddBookToGroupCommand command, CancellationToken cancellationToken)
    {
        var bookToAdd = await _booksRepository.GetByIdAsync(command.BookId, cancellationToken);

        if (bookToAdd is null)
        {
            return new Result<string>(new NotFoundError("Book"));
        }
        
        var group = await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new NotFoundError("group"));
        }

        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new BadRequestError("Only admin can add books to group"));
        }
        
        if (group.AllowedBooks.Any(book => book.Id == command.BookId))
        {
            return new Result<string>(new BadRequestError("Book is already allowed in this group"));
        }
        
        group.AddBook(bookToAdd);
        
        await _groupsRepository.UpdateAsync(group, cancellationToken);
        await _groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}