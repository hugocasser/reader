using Application.Abstractions.Repositories;
using Domain.Abstractions.Events;
using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class EventsRepository(MongoDbContext _dbContext) : IEventsRepository<Book>
{
    public async Task AddEventAsync(GenericDomainEvent<Book> eventModel, CancellationToken cancellationToken)
    {
        await _dbContext.EventsCollection.InsertOneAsync(eventModel, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<GenericDomainEvent<Book>>> GetNotProcessedEventsAsync(int count, CancellationToken cancellationToken)
    {
        return await _dbContext.EventsCollection
            .Find(eventToProcess => eventToProcess.ProcessedAt == null)
            .Limit(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<UpdateResult> UpdateProcessedEventAsync(GenericDomainEvent<Book> processedEvent,
        CancellationToken cancellationToken)
    {
        return  await _dbContext.EventsCollection.
            UpdateOneAsync(ev => ev.Id == processedEvent.Id,
                new UpdateDefinitionBuilder<GenericDomainEvent<Book>>()
                    .Set(x => x.ProcessedAt, DateTime.Now),
                cancellationToken: cancellationToken);
    }
}