using Domain.DomainEvents;
using MediatR;

namespace Domain.Abstractions;

public interface IDomainEvent : INotification
{
    public EventType EventType { get; init; }
    public Entity Entity { get; init; }
}