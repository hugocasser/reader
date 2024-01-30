using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class BooksRepository(MongoDbContext dbContext) : AbstractRepository(dbContext), IBooksRepository
{
    public async Task AddBookAsync(Book book, CancellationToken cancellationToken)
    {
        await DbContext.BooksCollection.InsertOneAsync(book, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(int take, int skip, CancellationToken cancellationToken)
    {
        return await DbContext.BooksCollection.Find(_ => true)
            .Skip(skip).Limit(take).SortBy(book => book.Name).ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.BooksCollection.Find(book => book.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken)
    {
        await DbContext.BooksCollection.DeleteOneAsync(book => book.Id == id, cancellationToken);
    }

    public async Task UpdateBookAsync(Book book, CancellationToken cancellationToken)
    {
        await DbContext.BooksCollection.ReplaceOneAsync(oldBook => oldBook.Id == book.Id, book, cancellationToken: cancellationToken);
    }

    public async Task<bool> BookExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await DbContext.BooksCollection.Find(book => book.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        return book is not null;
    }

    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId,
        int take, int skip, CancellationToken cancellationToken)
    {
        return await DbContext.BooksCollection.Find(book => book.CategoryId == categoryId)
            .Skip(skip).Limit(take).SortBy(book => book.Name).ToListAsync(cancellationToken);
    }
}