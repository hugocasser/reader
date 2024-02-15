using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class CategoriesRepository(MongoDbContext _dbContext) : BaseRepository<Category>(_dbContext.CategoriesCollection),
    ICategoriesRepository
{
    public async Task<Category> GetCategoryByNameAsync(string name, CancellationToken cancellationToken)
    {
         return await _dbContext.CategoriesCollection
            .Find(category => category.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}