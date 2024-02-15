using Application.Abstractions.Repositories;
using Domain.DomainEvents.Users;
using MediatR;

namespace Application.Events.Users;

public class UserDeletedEventHandler(IUsersRepository _usersRepository) : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _usersRepository.DeleteByIdAsyncInReadDbContext(notification.Id, cancellationToken);
    }
}