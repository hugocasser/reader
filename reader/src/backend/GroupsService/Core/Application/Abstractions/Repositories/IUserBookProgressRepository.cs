using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IUserBookProgressRepository
{
    public Task CreateProgressAsync(UserBookProgress userBookProgress);
    public Task<IEnumerable<UserBookProgress>> GetProgressesByUserIdAsync(Guid userId, PageSettings pageSettings);
    public Task DeleteProgressAsync(UserBookProgress userBookProgress);
    public Task UpdateProgressAsync(UserBookProgress userBookProgress);
    public Task<IEnumerable<UserBookProgress>> GetProgressesByGroupIdAsync(Guid groupId);
    public Task<UserBookProgress?> GetProgressByUserIdAndBookIdAsync(Guid userId, Guid bookId);
    public Task<UserBookProgress?> GetProgressByUserIdBookIdAndGroupIdAsync(Guid userId, Guid bookId, Guid groupId);
    public Task<UserBookProgress?> GetProgressByIdAsync(Guid requestUserBookProgressId);
    public Task SaveChangesAsync();
}