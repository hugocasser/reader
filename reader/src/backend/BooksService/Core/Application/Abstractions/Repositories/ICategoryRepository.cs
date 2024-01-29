using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICategoryRepository
{
    public Task AddCategoryAsync(Category category);
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(Guid id);
    public Task DeleteByIdCategoryAsync(Guid id);
    public Task<bool> CategoryExistsAsync(Guid id);
    public Task<Category> GetCategoryByNameAsync(string name);
}