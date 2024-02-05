using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class AuthorsRepository(MongoDbContext dbContext) : AbstractRepository(dbContext), IAuthorsRepository
{
    public async Task<IEnumerable<Author>> GetAuthorsAsync(int take, int skip,CancellationToken cancellationToken)
    {
        return await DbContext.AuthorsCollection.Find(_ => true)
            .Skip(skip).Limit(take).SortBy(author => author.FirstName)
            .ThenBy(author => author.LastName).ToListAsync(cancellationToken);
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.AuthorsCollection.Find(author => author.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAuthorAsync(Author author, CancellationToken cancellationToken)
    {
        await DbContext.AuthorsCollection.InsertOneAsync(author, cancellationToken:cancellationToken);
    }

    public async Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        await DbContext.AuthorsCollection.DeleteOneAsync(author => author.Id == id, cancellationToken);
    }

    public async Task UpdateAuthorAsync(Author author, CancellationToken cancellationToken)
    {
        await DbContext.AuthorsCollection.ReplaceOneAsync(oldAuthor => oldAuthor.Id == author.Id,
            author, cancellationToken: cancellationToken);
    }

    public async Task<bool> AuthorExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.AuthorsCollection.Find(author => author.Id == id).AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken)
    {
        return await DbContext.BooksCollection.Find(book => book.AuthorId == authorId).ToListAsync(cancellationToken);
    }
}