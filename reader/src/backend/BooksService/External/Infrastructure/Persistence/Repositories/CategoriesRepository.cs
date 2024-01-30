using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class CategoriesRepository(MongoDbContext dbContext) : AbstractRepository(dbContext), ICategoriesRepository
{
    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await DbContext.CategoriesCollection.InsertOneAsync(category, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(int take, int skip, CancellationToken cancellationToken)
    {
        return await DbContext.CategoriesCollection.Find(_ => true)
            .Skip(skip).Limit(take).SortBy(category => category.Name).ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    { 
        return await DbContext.CategoriesCollection
            .Find(category => category.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteByIdCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        await DbContext.CategoriesCollection.DeleteOneAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<bool> CategoryExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.CategoriesCollection.Find(category => category.Id == id).AnyAsync(cancellationToken);
    }

    public async Task<Category> GetCategoryByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbContext.CategoriesCollection.Find(category => category.Name == name)
            .FirstOrDefaultAsync(cancellationToken);
    }
}