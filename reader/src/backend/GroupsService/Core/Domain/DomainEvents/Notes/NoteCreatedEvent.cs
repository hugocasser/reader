using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Notes;

public record NoteCreatedEvent(Note Entity) : IDomainEvent;