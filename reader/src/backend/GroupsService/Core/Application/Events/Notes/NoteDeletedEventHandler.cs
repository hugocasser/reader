using Application.Abstractions.Repositories;
using Domain.DomainEvents.Notes;
using MediatR;

namespace Application.Events.Notes;

public class NoteDeletedEventHandler(INotesRepository _notesRepository) : INotificationHandler<NoteDeletedEvent>
{
    public async Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _notesRepository.DeleteByIdAsyncInReadDbContext(notification.Id, cancellationToken);
    }
}