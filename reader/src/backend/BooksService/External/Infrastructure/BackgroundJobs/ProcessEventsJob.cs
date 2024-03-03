using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessEventsJob
    (IEventsRepository<Book> _eventsRepository,
        IKafkaProducerService<Book> _kafkaProducerService, ILogger<ProcessEventsJob> _logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
     _logger.LogInformation(" --> Start sending events to Kafka. <--");
     
        var events = await 
            _eventsRepository.GetNotProcessedEventsAsync(50, context.CancellationToken);

        foreach (var eventToProcess in events)
        {
            await _kafkaProducerService.SendEventAsync(eventToProcess);
            eventToProcess.ProcessedAt = DateTime.Now;
            await _eventsRepository.UpdateProcessedEventAsync(eventToProcess, context.CancellationToken);
        }
        
        _logger.LogInformation("--> End sending events to Kafka. <--");
    }
}