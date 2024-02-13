using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveBookFromGroup;

public class RemoveBookFromGroupRequestHandler(IGroupsRepository _groupsRepository,
    IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<RemoveBookFromGroupRequest, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveBookFromGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);
        
        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (request.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new Error("You are not admin of this group", 400));
        }

        var book = group.AllowedBooks.FirstOrDefault(book => book.Id == request.BookId);
        
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