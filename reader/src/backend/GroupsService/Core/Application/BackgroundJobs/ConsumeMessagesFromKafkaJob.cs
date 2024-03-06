using Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

public class ConsumeMessagesFromKafkaJob(ILogger<ConsumeMessagesFromKafkaJob> _logger,
    IKafkaConsumerService _kafkaConsumerService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("--> Started consuming and processing messages...");
        _logger.LogInformation("DateTime: " + DateTime.Now);
        await _kafkaConsumerService.Consume(context.CancellationToken);
        
        _logger.LogInformation("--> Finished consuming messages...");
    }
}