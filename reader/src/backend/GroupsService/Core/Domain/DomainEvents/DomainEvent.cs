using Domain.Abstractions;

namespace Domain.DomainEvents;

public sealed record DomainEvent(EventType EventType, Entity Entity) : IDomainEvent;