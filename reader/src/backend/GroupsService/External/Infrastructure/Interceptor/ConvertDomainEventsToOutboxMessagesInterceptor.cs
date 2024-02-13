using Domain.Abstractions;
using Infrastructure.OutboxMessages;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity
                    .GetDomainEvents();
                
                entity.ClearDomainEvents();
                
                return domainEvents;
            })
            .Select(
                domainEvent => new OutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    IsProcessed = false,
                    EventType = domainEvent.EventType,
                    Entity = domainEvent.Entity
                });
        
        context.Set<OutboxMessage>().AddRange(messages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}