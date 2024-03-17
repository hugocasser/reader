using Application.Abstractions.Repositories;
using Domain.DomainEvents.UserProgresses;
using MediatR;

namespace Application.EventHandlers.UserBookProgresses;

public class UserBookProgressCreatedEventHandler(IUserBookProgressRepository _userBookProgressRepository) 
    : INotificationHandler<UserBookProgressCreatedEvent>

{
    public async Task Handle(UserBookProgressCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _userBookProgressRepository.CreateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}