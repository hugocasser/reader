using Application.Dtos;
using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IBooksRepository
{
    public Task AddBook(Book book);
    public Task DeleteBook(Book book);
    public Task UpdateBook(Book book);
    public Task<Book?> GetBookByIdAsync(Guid bookId);
    public Task<IEnumerable<Book>> GetAllBooks(PageSettings pageSettings);
}