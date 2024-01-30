using Application.Common;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views.Books;

namespace Application.Abstractions.Services;

public interface IBooksService
{
    public Task CreateBookAsync(CreateBookRequest book, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoView>> GetAllBooksAsync(PageSettings pageSettings,
        CancellationToken cancellationToken);
    public Task<BookView> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<BookInfoView> GetBookInfoByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateBookInfoAsync(UpdateBookRequest request, CancellationToken cancellationToken);
    public Task UpdateBookTextAsync(UpdateBookTextRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoView>> GetAllAuthorBooksAsync(Guid authorId,
        PageSettings pageSettings, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoView>> GetAllCategoryBooksAsync(Guid categoryId,
        PageSettings pageSettings, CancellationToken cancellationToken);
}