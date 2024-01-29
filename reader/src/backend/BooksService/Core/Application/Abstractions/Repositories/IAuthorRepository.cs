using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IAuthorRepository
{
    public Task<IEnumerable<Author>> GetAuthorsAsync();
    public Task<Author?> GetAuthorByIdAsync(Guid id);
    public Task AddAuthorAsync(Author author);
    public Task DeleteByIdAuthorAsync(Guid id);
    public Task UpdateAuthorAsync(Author author);
    public Task<bool> AuthorExistsAsync(Guid id);
}