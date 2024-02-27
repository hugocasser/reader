using Infrastructure.Persistence;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(
    WriteDbContext writeDbContext,
    ReadDbContext readDbContext,
    ILogger<ProcessOutboxMessagesJob> logger,
    IPublisher publisher,
    IMapper _mapper)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation($"--- Started syncing databases --- \n" +
            $"DateTime: {DateTime.Now}");
        
        var messages = await writeDbContext.OutboxMessages
            .Where(message => message.ProcessedAt == null)
            .Take(50)
            .ToListAsync(context.CancellationToken);
        
        foreach (var message in messages)
        {
            
            var domainEvent = JsonConvert.DeserializeObject(message.Content);
            var type = Type.GetType(message.EventType);
            var mappedEvent = _mapper.Map(domainEvent, domainEvent.GetType(), type);

            try
            {
                await publisher.Publish(mappedEvent, context.CancellationToken);
                message.ProcessedAt = DateTime.Now;
                logger.LogInformation($"Message {message.Id} processed at {DateTime.Now}");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
        
        await readDbContext.SaveChangesAsync(context.CancellationToken);

        logger.LogInformation($"--- Finished syncing databases --- \n" +
            $"DateTime: {DateTime.Now}");
    }
}