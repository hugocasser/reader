using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.DataServices;

public class RefreshTokensService(
    IRefreshTokensRepository refreshTokensRepository,
    IUsersRepository usersRepository,
    IRefreshTokenGeneratorService refreshTokenService,
    IAuthTokenGeneratorService authService)
    : IRefreshTokensService
{
    public async Task<IEnumerable<RefreshToken>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await refreshTokensRepository.GetAllAsync(cancellationToken);
    }

    public async Task<AuthTokens> UpdateAuthToken(CancellationToken cancellationToken,
        UpdateAuthTokenRequestDto updateAuthTokenRequestDto)
    {
        var refreshToken = await refreshTokenService.ValidateTokenAsync(updateAuthTokenRequestDto.UserId,
            updateAuthTokenRequestDto.RefreshToken, cancellationToken);
        var user = await usersRepository.GetUserByIdAsync(refreshToken.UserId);
        
        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("There is no user with this Id");
        }
        
        var userRoles = await usersRepository.GetUserRolesAsync(user);
        var userToken = authService.GenerateToken(user.Id, user.Email, userRoles);
        
        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }
}