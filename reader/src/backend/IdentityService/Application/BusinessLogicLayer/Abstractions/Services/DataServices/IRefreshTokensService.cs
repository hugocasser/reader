using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.DataServices;

public interface IRefreshTokensService
{
    public Task<IEnumerable<RefreshToken>> GetAllAsync(CancellationToken cancellationToken);
    public Task<AuthTokens> UpdateAuthToken(CancellationToken cancellationToken,
        UpdateAuthTokenRequestDto updateAuthTokenRequestDto);
}