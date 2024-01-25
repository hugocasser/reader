using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.AuthServices;

public class RefreshTokenGeneratorService : IRefreshTokenGeneratorService
{
    private readonly IRefreshTokensRepository _refreshTokensRepository;

    public RefreshTokenGeneratorService(IRefreshTokensRepository refreshTokensRepository)
    {
        _refreshTokensRepository = refreshTokensRepository;
    }

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
        var refreshToken = await _refreshTokensRepository.FindUserTokenAsync(userId, token, cancellationToken);
        if (refreshToken is null)
        {
            throw new IdentityExceptionWithStatusCode("The refresh token was not generated.");
        }

        if (refreshToken.ExpiryTime < DateTime.UtcNow)
        {
            throw new IdentityExceptionWithStatusCode("The refresh token was expired or revoked. Please login again");
        }

        return refreshToken;
    }
}