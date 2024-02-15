using Domain.Abstractions.Events;
using Domain.DomainEvents;
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
        var messages = await _writeDbContext.OutboxMessages
            .Where(message => message.ProcessedAt == null)
            .Take(50)
            .ToListAsync(context.CancellationToken);
        
        foreach (var message in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content);

            if (domainEvent is null)
            {
                _logger.LogInformation($"event is null \n" +
                    $"Event name:{nameof(domainEvent)}\n" +
                    $"DateTime:{DateTime.Now}");
                return;
            }
            
            await _publisher.Publish(domainEvent, context.CancellationToken);
            message.ProcessedAt = DateTime.Now;
            _logger.LogInformation($"Message {message.Id} processed at {DateTime.Now}");
            
            await _readDbContext.SaveChangesAsync(context.CancellationToken);
        }
    }
}