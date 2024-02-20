using Application.Abstractions.Repositories;
using Domain.DomainEvents.Books;
using MediatR;

namespace Application.Events.Books;

public class BookUpdatedEventHandler(IBooksRepository _booksRepository) : INotificationHandler<BookUpdatedEvent>
{
    public async Task Handle(BookUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _booksRepository.UpdateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}