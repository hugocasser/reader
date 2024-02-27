namespace Application.Abstractions.Services;

public interface IKafkaConsumerService
{
    public Task Consume(CancellationToken cancellationToken);
    protected Task ProcessMessage(string stringId, string stringMessage, string eventType, CancellationToken cancellationToken);
}