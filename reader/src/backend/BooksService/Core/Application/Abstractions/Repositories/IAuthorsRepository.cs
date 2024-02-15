using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IAuthorsRepository : IBaseRepository<Author>
{
    public Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken);
}