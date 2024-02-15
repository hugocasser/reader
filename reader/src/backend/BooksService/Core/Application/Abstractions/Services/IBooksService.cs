using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views.Books;

namespace Application.Abstractions.Services;

public interface IBooksService
{
    public Task<BookInfoViewDto> CreateBookAsync(CreateBookRequestDto book, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoViewDto>> GetAllBooksAsync(PageSettingRequestDto pageSettingsRequestDto,
        CancellationToken cancellationToken);
    public Task<BookViewDto> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<BookInfoViewDto> GetBookInfoByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken);
    public Task<BookInfoViewDto> UpdateBookInfoAsync(UpdateBookInfoRequestDto infoRequestDto, CancellationToken cancellationToken);
    public Task<BookViewDto> UpdateBookTextAsync(UpdateBookTextRequestDto requestDto, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoViewDto>> GetAllAuthorBooksAsync(Guid authorId,
        PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken);
    public Task<IEnumerable<BookShortInfoViewDto>> GetAllCategoryBooksAsync(Guid categoryId,
        PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken);
}