using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IAuthorsRepository
{
    public Task<IEnumerable<Author>> GetAuthorsAsync(int take, int skip, CancellationToken cancellationToken);
    public Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task AddAuthorAsync(Author author, CancellationToken cancellationToken);
    public Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
    public Task<bool> AuthorExistsAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken);
}