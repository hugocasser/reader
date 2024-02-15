using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.DataServices;

public interface IRefreshTokensService
{
    public Task<IEnumerable<RefreshToken>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    public Task<AuthTokens> RefreshTokenAsync
        (UpdateAuthTokenRequestDto updateAuthTokenRequestDto,CancellationToken cancellationToken);
}