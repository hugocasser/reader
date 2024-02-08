using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveBookFromGroup;

public class RemoveBookFromGroupRequestHandler(IGroupsRepository groupsRepository, IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<RemoveBookFromGroupRequest>
{
    public async Task Handle(RemoveBookFromGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId)
            ?? throw new NotFoundException("Group not found");

        if (request.UserId != group.AdminId)
        {
            throw new BadRequestException("You are not the admin of this group");
        }

        groupsRepository.RemoveBookFromGroupAsync(request.BookId);
    }
}