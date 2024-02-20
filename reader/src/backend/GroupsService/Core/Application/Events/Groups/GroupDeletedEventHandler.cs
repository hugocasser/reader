using Application.Abstractions.Repositories;
using Domain.DomainEvents.Groups;
using MediatR;

namespace Application.Events.Groups;

public class GroupDeletedEventHandler(IGroupsRepository _groupsRepository) : INotificationHandler<GroupDeletedEvent>
{
    public async Task Handle(GroupDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _groupsRepository.DeleteByIdAsyncInReadDbContextAsync(notification.Id, cancellationToken);
    }
}