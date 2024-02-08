using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Groups.AddBookToGroup;

public class AddBookToGroupRequestHandler(IBooksRepository booksRepository, IGroupsRepository groupsRepository)
    : IRequestHandler<AddBookToGroupRequest>
{
    public async Task Handle(AddBookToGroupRequest request, CancellationToken cancellationToken)
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

        if (request.UserId != group.AdminId)
        {
            throw new BadRequestException("You are not the admin of this group");
        }
        
        if (group.AllowedBooks.Any(book => book.Id == request.BookId))
        {
            throw new BadRequestException("Book already in group");
        }
        
        group.AllowedBooks.Add(bookToAdd);
        
        await groupsRepository.UpdateGroupAsync(group);
        await groupsRepository.SaveChangesAsync();
    }
}