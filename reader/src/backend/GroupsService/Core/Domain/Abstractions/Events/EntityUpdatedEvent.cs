using Domain.Abstractions;
using Domain.Abstractions.Events;

namespace Domain.DomainEvents;

public record EntityUpdatedEvent<T>(T Entity) : IDomainEvent where T : Entity;