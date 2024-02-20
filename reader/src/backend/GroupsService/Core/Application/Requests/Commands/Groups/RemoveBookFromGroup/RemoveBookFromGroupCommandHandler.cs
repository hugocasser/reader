using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.RemoveBookFromGroup;

public class RemoveBookFromGroupCommandHandler(IGroupsRepository _groupsRepository,
    IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<RemoveBookFromGroupCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveBookFromGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);
        
        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new Error("You are not admin of this group", 400));
        }

        var book = group.AllowedBooks.FirstOrDefault(book => book.Id == command.BookId);
        
        if ( book is null)
        {
            return new Result<string>(new Error("Book isn't allowed in this group", 404));
        }

        group.RemoveBook(book);

        await _groupsRepository.UpdateAsync(group, cancellationToken);
        await _groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Book removed from group");
    }
}