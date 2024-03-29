using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services.DataServices;

public class RefreshTokensService(
    IRefreshTokensRepository _refreshTokensRepository,
    UserManager<User> _userManager,
    IRefreshTokenGeneratorService _refreshTokenService,
    IAuthTokenGeneratorService _authService)
    : IRefreshTokensService
{

    public async Task<IEnumerable<RefreshToken>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _refreshTokensRepository.GetAllAsync((page-1)*pageSize, pageSize, cancellationToken);
    }

    public async Task<AuthTokens> RefreshTokenAsync(UpdateAuthTokenRequestDto updateAuthTokenRequestDto,CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenService.ValidateTokenAsync(updateAuthTokenRequestDto.UserId,
            updateAuthTokenRequestDto.RefreshToken, cancellationToken); ;
        var user = await _userManager.FindByIdAsync(updateAuthTokenRequestDto.UserId.ToString());
        
        if (user is null)
        {
            throw new NotFoundException("There is no user with this Id");
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var userToken = _authService.GenerateToken(user.Id, user.Email, userRoles);
        
        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }
}