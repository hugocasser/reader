using Application.Abstractions.Repositories;
using Application.Common;
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
            return new Result<string>(new Error("Book not found", 404));
        }
        
        var group = await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new Error("Only admin can add books to group", 400));
        }
        
        if (group.AllowedBooks.Any(book => book.Id == command.BookId))
        {
            return new Result<string>(new Error("Book is already allowed in this group", 400));
        }
        
        group.AddBook(bookToAdd);
        
        await _groupsRepository.UpdateAsync(group, cancellationToken);
        await _groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Book added to group");
    }
}