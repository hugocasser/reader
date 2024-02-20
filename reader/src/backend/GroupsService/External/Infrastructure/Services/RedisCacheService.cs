using System.Text.Json;
using Application.Abstractions.Services;
using Application.Abstractions.Services.Cache;
using Infrastructure.Configurations;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class RedisCacheService(IConnectionMultiplexer _connection) : IRedisCacheService
{
    private readonly IDatabase _database = _connection.GetDatabase();
    public async Task<T?> CreateAsync<T>(string key, Func<Task<T?>> factory)
    {
        var value = await factory.Invoke();
        if (value is null)
        {
            return value;
        }
        
        await _database.StringSetAsync(key, JsonSerializer.Serialize(value), DistributedCacheConfiguration.CacheExpiryTime);
        
        return value;
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T?>> factory)
    {
        var cachedMember = await _database.StringGetAsync(key);
        if (cachedMember.IsNullOrEmpty)
        {
            return await CreateAsync(key, factory);
        }

        return JsonSerializer.Deserialize<T>(cachedMember!);
    }

    public async Task SetAddRangeAsync<T>(string key, Func<IQueryable<T>> factory)
    {
        var values = factory.Invoke();
        
        foreach (var value in values)
        {
            await _database.SetAddAsync(key, JsonSerializer.Serialize(value));
        }
    }

    public async IAsyncEnumerable<T> GetSetOrAddRangeAsync<T>(string key, Func<IQueryable<T>> factory)
    {
        var length = _database.SetLength(key);
        if (length <= 0)
        {
            var values = factory.Invoke();
            foreach (var value in values)
            {
                await _database.SetAddAsync(key, JsonSerializer.Serialize(value));
                
                yield return value;
            }
        }
        
        foreach (var member in await _database.SetMembersAsync(key))
        {
            yield return JsonSerializer.Deserialize<T>(member);
        }
    }

    public async IAsyncEnumerable<T> GetSetAsync<T>(string key)
    {
        foreach (var member in  await _database.SetMembersAsync(key))
        {
            yield return JsonSerializer.Deserialize<T>(member);
        }
    }

    public void SetRemoveMember<T>(string key, T member) where T : IEquatable<T>
    {
        _database.SetRemove(key, JsonSerializer.Serialize(member));
    }

    public async Task SetAddAsync<T>(string key, Func<Task<T?>> factory)
    {
        var value = await factory.Invoke();
        if (value is null)
        {
            return;
        }
        
        await _database.SetAddAsync(key, JsonSerializer.Serialize(value));
    }

    public async Task SetAddAsync<T>(string key, T obj)
    {
        await _database.SetAddAsync(key, JsonSerializer.Serialize(obj));
    }

    public IQueryable<T> GetSetOrAddRangeQueryableAsync<T>(string key, Func<IQueryable<T>> factory)
    {
        _database.KeyExpire(key, DistributedCacheConfiguration.CacheExpiryTime);
        var collection = GetSetOrAddRangeAsync(key, factory);
        
        return collection.ToBlockingEnumerable().AsQueryable();
    }
}