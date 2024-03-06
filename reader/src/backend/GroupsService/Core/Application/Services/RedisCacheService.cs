using System.Text.Json;
using Application.Abstractions.Services.Cache;
using Application.Common;
using Application.Configurations;
using Domain.Abstractions;
using Domain.Models;
using StackExchange.Redis;

namespace Application.Services;

public class RedisCacheService(IConnectionMultiplexer _connection) : IRedisCacheService
{
    private readonly IDatabase _database = _connection.GetDatabase();
    public async Task CreateAsync(Entity entity)
    {
        await _database.StringSetAsync(CachingKeys.NoteById(entity.Id), JsonSerializer.Serialize(entity));
    }

    public async Task<bool> RemoveAsync(Guid key)
    {
        return await _database.KeyDeleteAsync(CachingKeys.NoteById(key));
    }

    public async Task RemoveRangeAsync(IEnumerable<Guid> keys)
    {
        foreach (var key in keys)
        {
            await _database.KeyDeleteAsync(CachingKeys.NoteById(key));   
        }
    }

    public async Task<Note?> GetByIdAsync(Guid key)
    {
        var note = await _database.StringGetAsync(CachingKeys.NoteById(key));
        
        return note.IsNull ? null : JsonSerializer.Deserialize<Note?>(note);
    }

    public async Task<IEnumerable<Note?>> GetRangeAsync(int count)
    {
        var lst = await _database.ListRangeAsync(CachingKeys.Notes, 0, long.Parse(count.ToString()));
        var notes = lst.Select(note => JsonSerializer.Deserialize<Note?>(note.ToString()));
        
        return notes;
    }
}