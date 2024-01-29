using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Application.Dtos.Views.Authors;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IAuthorsService
{
    public Task CreateAuthorAsync(CreateAuthorRequest author);
    public Task<IEnumerable<AuthorShortView>> GetAllAuthorsAsync(PageSettings pageSettings);
    public Task<Author> GetAuthorByIdAsync(Guid id);
    public Task DeleteByIdAuthorAsync(Guid id);
    public Task UpdateAuthorAsync(UpdateAuthorsRequest request);
}