using Domain.Abstractions.Events;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(WriteDbContext _writeDbContext, ReadDbContext _readDbContext,
    ILogger<ProcessOutboxMessagesJob> _logger, IPublisher _publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"--- Started syncing databases --- \n" +
            $"DateTime: {DateTime.Now}");
        var messages = await _writeDbContext.OutboxMessages
            .Where(message => message.ProcessedAt == null)
            .Take(50)
            .ToListAsync(context.CancellationToken);
        
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
                _writeDbContext.OutboxMessages.Update(message);
                
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
            _writeDbContext.OutboxMessages.Update(message);
            
            _logger.LogInformation($"Message {message.Id} processed at {DateTime.Now}");
        }
        await _writeDbContext.SaveChangesAsync(context.CancellationToken);
        await _readDbContext.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation($"--- Finished syncing databases --- \n" +
            $"DateTime: {DateTime.Now}");
    }
}