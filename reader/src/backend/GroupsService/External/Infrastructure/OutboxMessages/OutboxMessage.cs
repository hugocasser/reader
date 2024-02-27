namespace Infrastructure.OutboxMessages;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string Content { get; set; }
    public string EventType { get; set; }
}