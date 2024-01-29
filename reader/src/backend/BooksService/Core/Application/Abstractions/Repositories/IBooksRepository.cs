using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IBooksRepository
{
    public Task AddBookAsync(Book book);
    public Task<IEnumerable<Book>> GetBooksAsync();
    public Task<Book?> GetBookByIdAsync(Guid id);
    public Task DeleteByIdBookAsync(Guid id);
    public Task UpdateBookAsync(Book book);
    public Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
    public Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId);
    public Task<bool> BookExistsAsync(Guid id);
}