using Domain.Abstractions;
using Domain.Abstractions.Events;

namespace Domain.DomainEvents;

public record EntityDeletedEvent<T>(Guid Id) : IDomainEvent where T : Entity;