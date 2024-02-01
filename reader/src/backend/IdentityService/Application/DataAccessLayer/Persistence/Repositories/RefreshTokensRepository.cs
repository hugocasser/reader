using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Persistence.Repositories;

public class RefreshTokensRepository(UsersDbContext usersDbContext) : IRefreshTokensRepository
{
    public async Task<IEnumerable<RefreshToken>> GetAllAsync(int skip, int pageSize, CancellationToken cancellationToken)
    {
        return await usersDbContext.RefreshTokens.AsNoTracking().Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await usersDbContext.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public RefreshToken Update(RefreshToken token)
    {
        return usersDbContext.RefreshTokens.Update(token).Entity;
    }
    

    public async Task<RefreshToken> FindUserTokenAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        return await usersDbContext.RefreshTokens.SingleAsync(refreshToken => refreshToken.UserId == userId && refreshToken.Token == token, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await usersDbContext.SaveChangesAsync(cancellationToken);
    }

    public void RemoveToken(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        usersDbContext.RefreshTokens.Remove(refreshToken);
    }
}