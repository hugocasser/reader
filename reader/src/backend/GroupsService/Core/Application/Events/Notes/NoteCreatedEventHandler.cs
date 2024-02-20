using Application.Abstractions.Repositories;
using Domain.DomainEvents.Notes;
using MediatR;

namespace Application.Events.Notes;

public class NoteCreatedEventHandler(INotesRepository _notesRepository) : INotificationHandler<NoteCreatedEvent>
{
    public async Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _notesRepository.CreateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}