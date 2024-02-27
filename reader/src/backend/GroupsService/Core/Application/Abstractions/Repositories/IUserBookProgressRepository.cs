using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IUserBookProgressRepository: IBaseRepository<UserBookProgress, ProgressViewDto>
{
    public Task<IEnumerable<UserBookProgress>> GetProgressesByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    public Task<IEnumerable<UserBookProgress>?> GetProgressesByUserIdAndGroupIdAsync(Guid userId, Guid bookId,
        CancellationToken cancellationToken);
    public Task<UserBookProgress?> GetProgressByUserIdBookIdAndGroupIdAsync(Guid userId, Guid bookId, Guid groupId,
        CancellationToken cancellationToken);
}