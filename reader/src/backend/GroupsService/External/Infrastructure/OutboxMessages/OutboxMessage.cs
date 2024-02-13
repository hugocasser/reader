using Domain.Abstractions;
using Domain.DomainEvents;

namespace Infrastructure.OutboxMessages;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public bool IsProcessed { get; set; }
    public EventType EventType { get; set; }
    public Entity Entity { get; set; }
}