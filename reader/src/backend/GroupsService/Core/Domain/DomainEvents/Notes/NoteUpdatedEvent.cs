using Domain.Models;

namespace Domain.DomainEvents.Notes;

public record NoteUpdatedEvent(Note Entity) : EntityUpdatedEvent<Note>(Entity);