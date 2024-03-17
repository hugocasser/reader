using Application.Abstractions.Repositories;
using Domain.DomainEvents.Notes;
using MediatR;

namespace Application.EventHandlers.Notes;

public class NoteUpdatedEventHandler(INotesRepository _notesRepository) : INotificationHandler<NoteUpdatedEvent>
{
    public async Task Handle(NoteUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _notesRepository.UpdateAsyncInReadDbContextAsync(notification.Entity, cancellationToken);
    }
}