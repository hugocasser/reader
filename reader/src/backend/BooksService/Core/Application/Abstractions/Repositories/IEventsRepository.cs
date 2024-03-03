using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Models;
using MongoDB.Driver;

namespace Application.Abstractions.Repositories;

public interface IEventsRepository<T> where T : Entity
{
    public Task AddEventAsync(GenericDomainEvent<T> eventModel, CancellationToken cancellationToken);

    public Task<IEnumerable<GenericDomainEvent<T>>> GetNotProcessedEventsAsync(int count,
        CancellationToken cancellationToken);
    
    public Task<UpdateResult> UpdateProcessedEventAsync(GenericDomainEvent<Book> processedEvent,
        CancellationToken cancellationToken);
}