using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.Cache;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.AuthServices;

public class RefreshTokenGeneratorService(IRefreshTokensRepository _refreshTokensRepository, IRefreshTokensCacheService _refreshTokensCache)
    : IRefreshTokenGeneratorService
{
    public RefreshToken GenerateToken(Guid userId)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = Utilities.GenerateRandomString(7),
            AddedTime = DateTime.UtcNow,
            ExpiryTime = DateTime.UtcNow.AddMonths(2)
        };
    }

    public async Task<RefreshToken> ValidateTokenAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokensCache.GetAsync(userId, cancellationToken);
        
        if (refreshToken is not null)
        {
            if (refreshToken.ExpiryTime >= DateTime.UtcNow)
            {
                return refreshToken;
            }
            
            _refreshTokensRepository.RemoveToken(refreshToken, cancellationToken);
        }
        
        refreshToken = await _refreshTokensRepository.FindUserTokenAsync(userId, cancellationToken);
            
        if (refreshToken is null)
        {
            throw new IdentityException("The refresh token was not generated.");
        }
        
        if (refreshToken.ExpiryTime <= DateTime.UtcNow)
        {
            _refreshTokensRepository.RemoveToken(refreshToken, cancellationToken);
            throw new IdentityException("The refresh token was expired or revoked. Please login again");
        }
        
        await _refreshTokensCache.SetAsync(refreshToken, cancellationToken);
        
        return refreshToken;
    }
}