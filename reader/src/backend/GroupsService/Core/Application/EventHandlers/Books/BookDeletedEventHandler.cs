using Application.Abstractions.Repositories;
using Domain.DomainEvents.Books;
using MediatR;

namespace Application.EventHandlers.Books;

public class BookDeletedEventHandler(IBooksRepository _booksRepository) : INotificationHandler<BookDeletedEvent>
{
    public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _booksRepository.DeleteByIdAsyncInReadDbContextAsync(notification.Id, cancellationToken);
    }
}