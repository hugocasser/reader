using Application.Common;
using Domain.Abstractions;

namespace Application.Abstractions.Services;

public interface IDbSyncerService
{
    public Task SendEventAsync(EventType type, IEntity entity, CancellationToken cancellationToken);
}