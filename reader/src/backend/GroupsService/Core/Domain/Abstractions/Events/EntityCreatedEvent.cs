using Domain.Abstractions;
using Domain.Abstractions.Events;

namespace Domain.DomainEvents;

public record EntityCreatedEvent<T>(T Entity) 
    : IDomainEvent where T : Entity;