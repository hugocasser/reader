using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Models;
using MongoDB.Driver;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessEventsJob
    (IEventsRepository<Book> _eventsRepository,
        IKafkaService<Book> _kafkaService,
        IMongoClient _client) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using var session = await _client.StartSessionAsync(cancellationToken:context.CancellationToken);
        session.StartTransaction();
        
        var events = await 
            _eventsRepository.GetNotProcessedEventsAsync(50, context.CancellationToken);

        foreach (var eventToProcess in events)
        {
            await _kafkaService.SendEventAsync(eventToProcess);
            eventToProcess.ProcessedAt = DateTime.Now;
            await _eventsRepository.UpdateProcessedEventAsync(eventToProcess, context.CancellationToken);
        }
        
        await session.CommitTransactionAsync(cancellationToken: context.CancellationToken);
    }
}