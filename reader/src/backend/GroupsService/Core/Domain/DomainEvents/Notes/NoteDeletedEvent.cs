using Domain.Models;

namespace Domain.DomainEvents.Notes;

public record NoteDeletedEvent(Guid Id) : EntityDeletedEvent<Note>(Id);