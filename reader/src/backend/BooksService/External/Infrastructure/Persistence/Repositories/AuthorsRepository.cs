using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class AuthorsRepository(MongoDbContext _dbContext, IBooksRepository _booksRepository) : BaseRepository<Author>(_dbContext.AuthorsCollection), IAuthorsRepository
{
    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken)
    {
        var books = await _dbContext.BooksCollection
            .FindAsync(book => book.AuthorId == authorId, cancellationToken: cancellationToken);
        
        return await books.ToListAsync(cancellationToken);
    }
}