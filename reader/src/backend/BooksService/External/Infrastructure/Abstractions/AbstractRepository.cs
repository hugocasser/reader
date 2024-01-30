using Infrastructure.Persistence;

namespace Infrastructure.Abstractions;

public class AbstractRepository(MongoDbContext dbContext)
{
    protected MongoDbContext DbContext { get; set; } = dbContext;
}