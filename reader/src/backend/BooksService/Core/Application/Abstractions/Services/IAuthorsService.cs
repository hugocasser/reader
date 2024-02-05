using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Application.Dtos.Views.Authors;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IAuthorsService
{
    public Task CreateAuthorAsync(CreateAuthorRequest author, CancellationToken cancellationToken);
    public Task<IEnumerable<AuthorShortView>> GetAllAuthorsAsync(PageSetting pageSettings, CancellationToken cancellationToken);
    public Task<AuthorView> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateAuthorAsync(UpdateAuthorRequest request, CancellationToken cancellationToken);
}