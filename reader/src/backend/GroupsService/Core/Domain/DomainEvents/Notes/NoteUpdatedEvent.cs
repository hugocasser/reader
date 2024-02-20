using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Notes;

public record NoteUpdatedEvent(Note Entity) : IDomainEvent;