using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Groups;

public record GroupCreatedEvent(Group Entity) : IDomainEvent;