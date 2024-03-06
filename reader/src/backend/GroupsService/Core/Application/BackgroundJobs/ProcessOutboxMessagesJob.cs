using Application.Abstractions.Repositories;
using Domain.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Application.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(IOutboxRepository _outboxRepository,
    ILogger<ProcessOutboxMessagesJob> _logger, IPublisher _publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"--- Started syncing databases --- \n" +
            $"DateTime: {DateTime.Now}");
        var messages = await _outboxRepository.GetNotProcessedAsync(50, context.CancellationToken);
        
        foreach (var message in messages)
        {
            _logger.LogInformation($"--- Processing message {message.Id} at {DateTime.Now} --- \n");
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            if (domainEvent is null)
            {
                _logger.LogInformation($"event is null\n" +
                    $"Event name:{nameof(domainEvent)}\n" +
                    $"DateTime:{DateTime.Now}");
                
                message.ProcessedAt = DateTime.UtcNow;
                await _outboxRepository.UpdateAsync(message, context.CancellationToken);
                
                return;
            }

            try
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"--- Error while processing message" + $" {message.Id} at {DateTime.Now} --- \n"
                    + "Error: " + e.Message);
            }
            
            message.ProcessedAt = DateTime.UtcNow;
            await _outboxRepository.UpdateAsync(message, context.CancellationToken);
            
            _logger.LogInformation($"Message {message.Id} processed at {DateTime.Now}");
        }
        await _outboxRepository.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation($"--- Finished syncing databases --- \n" +
            $"DateTime: {DateTime.Now}");
    }
}