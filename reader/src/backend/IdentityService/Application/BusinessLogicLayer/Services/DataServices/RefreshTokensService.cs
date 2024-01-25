using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.DataServices;

public class RefreshTokensService : IRefreshTokensService
{
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IRefreshTokenGeneratorService _refreshTokenService;
    private readonly IAuthTokenGeneratorService _authService;

    public RefreshTokensService(IRefreshTokensRepository refreshTokensRepository,
        IUsersRepository usersRepository, IRefreshTokenGeneratorService refreshTokenService,
        IAuthTokenGeneratorService authService)
    {
        _refreshTokensRepository = refreshTokensRepository;
        _usersRepository = usersRepository;
        _refreshTokenService = refreshTokenService;
        _authService = authService;
    }
    public async Task<IEnumerable<RefreshToken>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _refreshTokensRepository.GetAllAsync(cancellationToken);
    }

    public async Task<AuthTokens> UpdateAuthToken(CancellationToken cancellationToken,
        UpdateAuthTokenRequestDto updateAuthTokenRequestDto)
    {
        var refreshToken = await _refreshTokenService.ValidateTokenAsync(updateAuthTokenRequestDto.UserId,
            updateAuthTokenRequestDto.RefreshToken, cancellationToken);
        var user = await _usersRepository.GetUserByIdAsync(refreshToken.UserId);
        
        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("There is no user with this Id");
        }
        
        var userRoles = await _usersRepository.GetUserRolesAsync(user);
        var userToken = _authService.GenerateToken(user.Id, user.Email, userRoles);
        
        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }
}