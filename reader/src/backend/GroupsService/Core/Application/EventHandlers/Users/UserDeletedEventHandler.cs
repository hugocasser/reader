using Application.Abstractions.Repositories;
using Domain.DomainEvents.Users;
using MediatR;

namespace Application.EventHandlers.Users;

public class UserDeletedEventHandler(IUsersRepository _usersRepository) : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _usersRepository.DeleteByIdAsyncInReadDbContextAsync(notification.Id, cancellationToken);
    }
}