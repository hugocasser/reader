using Application.Common;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICategoriesRepository
{
    public Task AddCategoryAsync(Category category, CancellationToken cancellationToken);
    public Task<IEnumerable<Category>> GetCategoriesAsync(int take, int skip, CancellationToken cancellationToken);
    public Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteByIdCategoryAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> CategoryExistsAsync(Guid id, CancellationToken cancellationToken);
    public Task<Category> GetCategoryByNameAsync(string name, CancellationToken cancellationToken);
}