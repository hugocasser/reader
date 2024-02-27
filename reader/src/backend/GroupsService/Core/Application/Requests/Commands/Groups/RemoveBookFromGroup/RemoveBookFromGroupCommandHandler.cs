using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
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
            return new Result<string>(new NotFoundError("Group"));
        }

        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new BadRequestError("You are not admin"));
        }

        var book = group.AllowedBooks.FirstOrDefault(book => book.Id == command.BookId);
        
        if ( book is null)
        {
            return new Result<string>(new BadRequestError("Book is not allowed in this group"));
        }

        group.RemoveBook(book);

        await _groupsRepository.UpdateAsync(group, cancellationToken);
        await _groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}