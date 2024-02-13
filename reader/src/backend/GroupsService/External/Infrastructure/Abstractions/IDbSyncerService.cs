namespace Infrastructure.Abstractions;

public interface IDbSyncerService
{
    public Task SendEventAsync(CancellationToken cancellationToken);
}