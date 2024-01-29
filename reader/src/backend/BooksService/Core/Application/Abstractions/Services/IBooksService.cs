using Application.Common;
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views;
using Application.Dtos.Views.Books;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IBooksService
{
    public Task CreateBookAsync(CreateBookRequest book);
    public Task<IEnumerable<BookShortInfoView>> GetAllBooksAsync(PageSettings pageSettings);
    public Task<BookView> GetBookByIdAsync(Guid id);
    public Task<BookInfoView> GetBookInfoByIdAsync(Guid id);
    public Task DeleteByIdBookAsync(Guid id);
    public Task UpdateBookInfoAsync(UpdateBookRequest request);
    public Task UpdateBookTextAsync(UpdateBookTextRequest request);
    public Task<IEnumerable<BookShortInfoView>> GetAllAuthorBooksAsync(Guid authorId, PageSettings pageSettings);
    public Task<IEnumerable<BookShortInfoView>> GetAllCategoryBooksAsync(Guid categoryId, PageSettings pageSettings);
}