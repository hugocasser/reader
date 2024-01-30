using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IBooksRepository
{
    public Task AddBookAsync(Book book, CancellationToken cancellationToken);
    public Task<IEnumerable<Book>> GetBooksAsync(int take, int skip, CancellationToken cancellationToken);
    public Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateBookAsync(Book book, CancellationToken cancellationToken);
    public Task<bool> BookExistsAsync(Guid id, CancellationToken  cancellationToken);
    public Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId, int take, int skip,
        CancellationToken cancellationToken);
}