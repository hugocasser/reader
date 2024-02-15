using Application.Abstractions.Repositories;
using Domain.DomainEvents.Books;
using MediatR;

namespace Application.Events.Books;

public class BookCreatedEventHandler(IBooksRepository _booksRepository) : INotificationHandler<BookCreatedEvent>
{
    public async Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _booksRepository.CreateAsyncInReadDbContext(notification.Entity, cancellationToken);
    }
}