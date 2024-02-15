using Application.Abstractions.Repositories;
using Domain.DomainEvents.UserProgresses;
using MediatR;

namespace Application.Events.UserBookProgresses;

public class UserBookProgressCreatedEventHandler(IUserBookProgressRepository _userBookProgressRepository) 
    : INotificationHandler<UserBookProgressCreatedEvent>

{
    public async Task Handle(UserBookProgressCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _userBookProgressRepository.CreateAsyncInReadDbContext(notification.Entity, cancellationToken);
    }
}