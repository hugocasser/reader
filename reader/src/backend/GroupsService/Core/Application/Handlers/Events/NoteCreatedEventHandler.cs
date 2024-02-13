using Domain.Abstractions;
using Domain.DomainEvents;
using MediatR;

namespace Application.Handlers.Events;

public class NoteCreatedEventHandler : INotificationHandler<DomainEvent>
{
    public Task Handle(DomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}