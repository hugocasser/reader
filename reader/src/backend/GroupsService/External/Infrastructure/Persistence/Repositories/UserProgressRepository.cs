using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserBookProgressRepository
    (WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<UserBookProgress, ProgressViewDto>(_writeDbContext, _readDbContext), IUserBookProgressRepository
{
    public async Task<IEnumerable<UserBookProgress>> GetProgressesByGroupIdAsync
        (Guid groupId, CancellationToken cancellationToken)
    {
        return await _readDbContext.UserBookProgresses
            .Where(progress => progress.GroupId == groupId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserBookProgress>?> GetProgressesByUserIdAndGroupIdAsync
        (Guid userId, Guid bookId, CancellationToken cancellationToken)
    {
        return await _readDbContext.UserBookProgresses
            .Where(progress => progress.UserId == userId && progress.BookId == bookId).ToListAsync(cancellationToken);
    }

    public async Task<UserBookProgress?> GetProgressByUserIdBookIdAndGroupIdAsync
        (Guid userId, Guid bookId, Guid groupId, CancellationToken cancellationToken)
    {
        return await _readDbContext.UserBookProgresses
            .FirstOrDefaultAsync(progress => progress.UserId == userId
                && progress.BookId == bookId && progress.GroupId == groupId, cancellationToken);
    }
}