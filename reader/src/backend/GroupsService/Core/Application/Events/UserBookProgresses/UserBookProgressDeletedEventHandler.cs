using Application.Abstractions.Repositories;
using Domain.DomainEvents.UserProgresses;
using MediatR;

namespace Application.Events.UserBookProgresses;

public class UserBookProgressDeletedEventHandler(IUserBookProgressRepository _userBookProgressRepository)
    : INotificationHandler<UserBookProgressDeletedEvent>
{
    public async Task Handle(UserBookProgressDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _userBookProgressRepository.DeleteByIdAsyncInReadDbContextAsync(notification.Id, cancellationToken);
    }
}