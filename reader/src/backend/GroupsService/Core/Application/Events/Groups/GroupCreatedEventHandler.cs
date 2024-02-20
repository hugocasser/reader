using Application.Abstractions.Repositories;
using Domain.DomainEvents.Groups;
using MediatR;

namespace Application.Events.Groups;

public class GroupCreatedEventHandler(IGroupsRepository _groupsRepository) : INotificationHandler<GroupCreatedEvent>
{
    public async Task Handle(GroupCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _groupsRepository.CreateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}