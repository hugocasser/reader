using Application.Abstractions.Repositories;
using Domain.DomainEvents.Users;
using MediatR;

namespace Application.Events.Users;

public class UserUpdatedEventHandler(IUsersRepository _usersRepository) : INotificationHandler<UserUpdatedEvent>
{
    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _usersRepository.UpdateAsyncInReadDbContext(notification.Entity, cancellationToken);
    }
}