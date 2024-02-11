using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IUserBookProgressRepository: IBaseRepository<UserBookProgress>
{
    public Task<IEnumerable<UserBookProgress>> GetProgressesByGroupIdAsync(Guid groupId);
    public Task<IEnumerable<UserBookProgress>?> GetProgressesByUserIdAndGroupIdAsync(Guid userId, Guid bookId);
    public Task<UserBookProgress?> GetProgressByUserIdBookIdAndGroupIdAsync(Guid userId, Guid bookId, Guid groupId);
}