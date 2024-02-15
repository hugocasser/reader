using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Persistence.Repositories;

public class RefreshTokensRepository : IRefreshTokensRepository
{
    private readonly UsersDbContext _usersDbContext;
    
    public RefreshTokensRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _usersDbContext.RefreshTokens.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await _usersDbContext.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public RefreshToken Update(RefreshToken token)
    {
        return _usersDbContext.RefreshTokens.Update(token).Entity;
    }

    public async Task<RefreshToken> FindUserTokenAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        return await _usersDbContext.RefreshTokens.SingleAsync(refreshToken => refreshToken.UserId == userId && refreshToken.Token == token, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _usersDbContext.SaveChangesAsync(cancellationToken);
    }
}