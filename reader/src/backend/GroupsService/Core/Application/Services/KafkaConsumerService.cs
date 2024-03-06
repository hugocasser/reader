using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.KafkaMessages;
using Confluent.Kafka;
using Domain.Models;
using Infrastructure.Options;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class KafkaConsumerService
    : IKafkaConsumerService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;
    
    public KafkaConsumerService(IMapper mapper,
        IBooksRepository booksRepository,
        IOptions<KafkaConsumerOptions> _kafkaConsumerOptions)
    {
        _consumer =
            new ConsumerBuilder<string, string>(new ConsumerConfig()
                {
                    BootstrapServers = _kafkaConsumerOptions.Value.BootstrapServers
                })
                .Build();
        _consumer.Subscribe(nameof(BooksEventsEnum.Created));
        _consumer.Subscribe(nameof(BooksEventsEnum.Deleted));
        _consumer.Subscribe(nameof(BooksEventsEnum.Updated));
        
        _booksRepository = booksRepository;
        _mapper = mapper;
    }

    public async Task Consume(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var result = _consumer.Consume(cancellationToken);
            await ProcessMessage(result.Message.Key,result.Message.Value, result.TopicPartition.Topic, cancellationToken);
        }
        
        _consumer.Close();
    }

    public Task ProcessMessage(string stringId, string stringMessage, string eventType, CancellationToken cancellationToken)
    {
        switch (eventType)
        {
            case nameof(BooksEventsEnum.Created):
            {
                var message = JsonConvert.DeserializeObject(stringMessage);
                var book = _mapper.Map<Book>(message);
                
                return _booksRepository.CreateAsync(book, cancellationToken);
            }
            case nameof(BooksEventsEnum.Updated):
            {          
                var message = JsonConvert.DeserializeObject(stringMessage);
                var book = _mapper.Map<Book>(message);
                return _booksRepository.UpdateAsync(book, cancellationToken);
            }
            case nameof(BooksEventsEnum.Deleted):
            {
                var id = Guid.Parse(stringId);
                return _booksRepository.DeleteByIdAsync(id, cancellationToken);
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(eventType), eventType, "Unknown topic");
            }
        }
    }
}