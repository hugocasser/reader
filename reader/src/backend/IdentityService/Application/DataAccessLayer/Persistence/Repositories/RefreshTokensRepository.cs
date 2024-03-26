using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Persistence.Repositories;

public class RefreshTokensRepository(UsersDbContext _usersDbContext) : IRefreshTokensRepository
{
    public async Task<IEnumerable<RefreshToken>> GetAllAsync(int skip, int pageSize, CancellationToken cancellationToken)
    {
        return await _usersDbContext.RefreshTokens.AsNoTracking().Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await _usersDbContext.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public RefreshToken Update(RefreshToken token)
    {
        return _usersDbContext.RefreshTokens.Update(token).Entity;
    }
    

    public async Task<RefreshToken> FindUserTokenAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _usersDbContext.RefreshTokens.AsNoTracking().SingleAsync(token => token.UserId == userId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _usersDbContext.SaveChangesAsync(cancellationToken);
    }

    public void RemoveToken(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        _usersDbContext.RefreshTokens.Remove(refreshToken);
    }
}