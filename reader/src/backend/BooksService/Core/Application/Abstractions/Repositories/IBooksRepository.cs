using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IBooksRepository : IBaseRepository<Book>
{
    public Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId, PageSettingRequestDto pageSettingRequestDto,
        CancellationToken cancellationToken);
}