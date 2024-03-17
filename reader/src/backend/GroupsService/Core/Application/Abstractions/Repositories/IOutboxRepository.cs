using Application.OutboxMessages;

namespace Application.Abstractions.Repositories;

public interface IOutboxRepository
{
    public Task<IEnumerable<OutboxMessage>> GetNotProcessedAsync(int count, CancellationToken cancellationToken);
    public Task UpdateAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}