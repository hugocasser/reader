using Application.Abstractions.Repositories;
using Domain.DomainEvents.Groups;
using MediatR;

namespace Application.Events.Groups;

public class GroupUpdatedEventHandler(IGroupsRepository _groupsRepository) : INotificationHandler<GroupUpdatedEvent>
{
    public async Task Handle(GroupUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _groupsRepository.UpdateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}