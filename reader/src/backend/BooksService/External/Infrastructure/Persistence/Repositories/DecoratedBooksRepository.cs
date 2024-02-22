using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Events;
using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class DecoratedBooksRepository(IBooksRepository _booksRepository,
    IEventsRepository<Book> _eventsRepository,
    IMongoClient _client) : IBooksRepository
{
    public async Task UpdateAsync(Book entity, CancellationToken cancellationToken)
    {
        using var session = await _client.StartSessionAsync(cancellationToken: cancellationToken);
        session.StartTransaction();
        
        await _booksRepository.UpdateAsync(entity, cancellationToken);
        await _eventsRepository.AddEventAsync(new GenericDomainEvent<Book>(entity, EventType.Updated), cancellationToken);
        
        await session.CommitTransactionAsync(cancellationToken);
    }

    public async Task AddAsync(Book entity, CancellationToken cancellationToken)
    {
        using var session = await _client.StartSessionAsync(cancellationToken: cancellationToken);
        session.StartTransaction();
        
        await _booksRepository.AddAsync(entity, cancellationToken);
        await _eventsRepository.AddEventAsync(new GenericDomainEvent<Book>(entity, EventType.Created), cancellationToken);
        
        await session.CommitTransactionAsync(cancellationToken);
    }

    public Task<IEnumerable<Book>> GetAllAsync(PageSettingRequestDto pageSettingRequestDto, CancellationToken cancellationToken)
    {
        return _booksRepository.GetAllAsync(pageSettingRequestDto, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using var session = await _client.StartSessionAsync(cancellationToken: cancellationToken);
        session.StartTransaction();
        
        await _booksRepository.DeleteByIdAsync(id, cancellationToken);
        await _eventsRepository.AddEventAsync(new GenericDomainEvent<Book>
            (new Book {Id = id}, EventType.Deleted), cancellationToken);
        
        await session.CommitTransactionAsync(cancellationToken);
    }

    public Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _booksRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _booksRepository.IsExistsAsync(id, cancellationToken);
    }

    public Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId, PageSettingRequestDto pageSettingRequestDto,
        CancellationToken cancellationToken)
    {
        return _booksRepository.GetBooksByCategoryAsync(categoryId, pageSettingRequestDto, cancellationToken);
    }
}