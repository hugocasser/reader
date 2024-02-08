using Application.Abstractions.Repositories;
using Application.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Groups.CreateGroup;

public class CreateGroupRequestHandler(IGroupsRepository groupsRepository, IUsersRepository usersRepository)
    : IRequestHandler<CreateGroupRequest>
{
    public async Task Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var admin = await usersRepository.GetUserByIdAsync(request.AdminId);
        
        if (admin is null)
        {
            throw new NotFoundException( "Admin not found");
        }

        var group = new Group
        {
            Id = Guid.NewGuid(),
            AdminId = request.AdminId,
            GroupName = request.GroupName
        };
        
        await groupsRepository.CreateGroupAsync(group);
        await groupsRepository.SaveChangesAsync();
    }
}