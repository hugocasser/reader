using Domain.Abstractions;
using Infrastructure.OutboxMessages;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Infrastructure.Interceptor;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor 
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;
        
        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var messages = context.ChangeTracker
            .Entries<Entity>()
            .ToList()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity
                    .GetDomainEvents();
                entity.ClearDomainEvents();
                
                return domainEvents;
            })
            .Select(
                domainEvent =>
                {
                    var mess = new OutboxMessage()
                    {
                        Id = Guid.NewGuid(),
                        ProcessedAt = null,
                        Content = JsonConvert.SerializeObject(domainEvent,
                            new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.All
                            }),
                        EventType = domainEvent.GetType().FullName ?? domainEvent.GetType().Namespace + '.'
                            + domainEvent.GetType().Name +", Domain"
                    };
                    return mess;
                });
        
        context.Set<OutboxMessage>().AddRange(messages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}