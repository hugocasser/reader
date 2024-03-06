using Application.Abstractions.Services;
using Application.Dtos.KafkaMessages;
using Application.Options;
using Confluent.Kafka;
using Domain.Abstractions.Events;
using Domain.Models;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Services;

public class KafkaProducerBooksService(
    IOptions<KafkaProducerOptions> _kafkaOptions,
    IMapper _mapper,
    ILogger<KafkaProducerBooksService> _logger)
    : IKafkaProducerService<Book>, IDisposable
{
    private readonly IProducer<string, string> _producer =  new ProducerBuilder<string, string>(new ProducerConfig()
    {
        BootstrapServers = _kafkaOptions.Value.BootstrapServers,
    }).Build();

    public async Task SendEventAsync(GenericDomainEvent<Book> bookEvent)
    {
        var message = _mapper.Map<BookMessage>(bookEvent.Entity);
        var result = await _producer.ProduceAsync(nameof(bookEvent.EventType), new Message<string, string>()
        {
            Key = bookEvent.Id.ToString(),
            Value = JsonConvert.SerializeObject(message,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }),
        });
        
        if (result.Status is not (PersistenceStatus.Persisted or PersistenceStatus.PossiblyPersisted))
        {
            _logger.LogError("Failed to send message to Kafka." +
                " Status: {Status}.", result.Status + $"Message: {bookEvent.Entity}.");
        }
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}