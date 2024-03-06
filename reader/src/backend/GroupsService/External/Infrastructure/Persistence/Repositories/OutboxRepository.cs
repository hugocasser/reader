using Application.Abstractions.Repositories;
using Application.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class OutboxRepository(WriteDbContext _writeDbContext) : IOutboxRepository
{
    public async Task<IEnumerable<OutboxMessage>> GetNotProcessedAsync(int count, CancellationToken cancellationToken)
    {
        return await _writeDbContext.OutboxMessages.Where(message => message.ProcessedAt == null).Take(count).ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken)
    {
        return Task.FromResult(_writeDbContext.OutboxMessages.Update(outboxMessage));
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }
}