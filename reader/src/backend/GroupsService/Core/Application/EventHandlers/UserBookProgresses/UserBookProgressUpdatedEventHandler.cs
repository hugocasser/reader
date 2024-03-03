using Application.Abstractions.Repositories;
using Domain.DomainEvents.UserProgresses;
using MediatR;

namespace Application.EventHandlers.UserBookProgresses;

public class UserBookProgressUpdatedEventHandler(IUserBookProgressRepository _userBookProgressRepository)
    : INotificationHandler<UserBookProgressUpdatedEvent>
{
    public async Task Handle(UserBookProgressUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _userBookProgressRepository.UpdateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}