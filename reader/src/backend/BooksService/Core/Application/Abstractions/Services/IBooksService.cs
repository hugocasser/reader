using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views.Books;

namespace Application.Abstractions.Services;

public interface IBooksService
{
    public Task CreateBookAsync(CreateBookRequest book, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoView>> GetAllBooksAsync(PageSetting pageSettings,
        CancellationToken cancellationToken);
    public Task<BookView> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<BookInfoView> GetBookInfoByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateBookInfoAsync(UpdateBookInfoRequest infoRequest, CancellationToken cancellationToken);
    public Task UpdateBookTextAsync(UpdateBookTextRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoView>> GetAllAuthorBooksAsync(Guid authorId,
        PageSetting pageSettings, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoView>> GetAllCategoryBooksAsync(Guid categoryId,
        PageSetting pageSettings, CancellationToken cancellationToken);
}