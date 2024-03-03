using System.Text.Json;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Interceptor;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor 
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
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
                    ProcessedAt = null,
                    Content = JsonConvert.SerializeObject(domainEvent,
                        new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
                }).ToList();
        
        context.Set<OutboxMessage>().AddRange(messages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}