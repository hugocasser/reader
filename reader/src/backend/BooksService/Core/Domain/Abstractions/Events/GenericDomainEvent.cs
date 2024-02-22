using Domain.Events;

namespace Domain.Abstractions.Events;

public sealed class GenericDomainEvent<T>(T entity, EventType eventType)
    where T : Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public T Entity { get; set; } = entity;
    public EventType EventType { get; set; } = eventType;
    public DateTime? ProcessedAt { get; set; } = null;
}