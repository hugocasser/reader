using Domain.Abstractions;
using Domain.Models;

namespace Domain.DomainEvents.Notes;

public record NoteCreatedEvent(Note Entity) : EntityCreatedEvent<Note>(Entity);