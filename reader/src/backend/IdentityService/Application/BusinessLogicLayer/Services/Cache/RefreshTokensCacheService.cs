using System.Text.Json;
using BusinessLogicLayer.Abstractions.Services.Cache;
using BusinessLogicLayer.Options;
using DataAccessLayer.Models;
using Garnet.client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Services.Cache;

public class RefreshTokensCacheService(IOptions<GarnetOptions> options, ILogger<RefreshTokensCacheService> _logger)
    : IRefreshTokensCacheService
{
    private readonly GarnetClient _cacheService = new(options.Value.Address, options.Value.Port, timeoutMilliseconds: options.Value.TimeoutMilliseconds);

    public async Task SetAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await _cacheService.ConnectAsync(cancellationToken);
        var pong =await _cacheService.PingAsync(cancellationToken);
        
        if (pong != "PONG")
        {
            _logger.LogError("Cache service is not available");
            
            return;
        }
        
        await _cacheService.PingAsync(cancellationToken);
        var serializedToken = JsonSerializer.Serialize(token);
        
        await _cacheService.StringSetAsync(token.Id.ToString(), serializedToken, cancellationToken);
    }

    public async Task<RefreshToken?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        await _cacheService.ConnectAsync(cancellationToken);
        var pong =await _cacheService.PingAsync(cancellationToken);
        
        if (pong != "PONG")
        {
            _logger.LogError("Cache service is not available");
            
            return null;
        }
        
        await _cacheService.PingAsync(cancellationToken);
        var serializedToken = await _cacheService.StringGetAsync(id.ToString(), cancellationToken);
        
        return serializedToken is null ? null : JsonSerializer.Deserialize<RefreshToken>(serializedToken);
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        await _cacheService.ConnectAsync(cancellationToken);
        var pong =await _cacheService.PingAsync(cancellationToken);
        
        if (pong != "PONG")
        {
            _logger.LogError("Cache service is not available");
            
            return;
        }
        
        _ = await _cacheService.KeyDeleteAsync(id.ToString(), cancellationToken);
    }
}