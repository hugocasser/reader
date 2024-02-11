using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveBookFromGroup;

public class RemoveBookFromGroupRequestHandler(IGroupsRepository groupsRepository, IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<RemoveBookFromGroupRequest, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveBookFromGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);
        
        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (request.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new Error("You are not admin of this group", 400));
        }

        if (group.AllowedBooks.FirstOrDefault(book => book.Id == request.BookId) is null)
        {
            return new Result<string>(new Error("Book isn't allowed in this group", 404));
        }
        
        await groupsRepository.RemoveBookFromGroupAsync(request.BookId);
        
        return new Result<string>("Book removed from group");
    }
}