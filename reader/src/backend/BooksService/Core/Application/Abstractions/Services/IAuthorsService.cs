using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Application.Dtos.Views.Authors;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IAuthorsService
{
    public Task<AuthorViewDto> CreateAuthorAsync(CreateAuthorRequestDto author, CancellationToken cancellationToken);
    public Task<IEnumerable<AuthorShortViewDto>> GetAllAuthorsAsync(PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken);
    public Task<AuthorViewDto> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken);
    public Task<AuthorViewDto> UpdateAuthorAsync(UpdateAuthorRequestDto requestDto, CancellationToken cancellationToken);
}