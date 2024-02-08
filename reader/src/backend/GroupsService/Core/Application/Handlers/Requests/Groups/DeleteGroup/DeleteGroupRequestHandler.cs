using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Groups.DeleteGroup;

public class DeleteGroupRequestHandler(IGroupsRepository groupsRepository) : IRequestHandler<DeleteGroupRequest>
{
    public async Task Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");
        }

        if (request.UserId != group.AdminId)
        {
            throw new BadRequestException("You are not the admin of this group");
        }
        
        await groupsRepository.DeleteGroupByIdAsync(request.GroupId);
        await groupsRepository.SaveChangesAsync();
    }
}