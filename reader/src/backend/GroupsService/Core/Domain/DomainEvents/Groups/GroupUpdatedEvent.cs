using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Groups;

public record GroupUpdatedEvent(Group Entity) : IDomainEvent;