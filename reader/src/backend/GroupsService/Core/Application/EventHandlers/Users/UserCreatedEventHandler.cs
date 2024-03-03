using Application.Abstractions.Repositories;
using Domain.DomainEvents.Users;
using MediatR;

namespace Application.EventHandlers.Users;

public class UserCreatedEventHandler(IUsersRepository _usersRepository) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _usersRepository.CreateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}