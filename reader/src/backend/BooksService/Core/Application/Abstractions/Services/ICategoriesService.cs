using Application.Dtos.Requests;
using Application.Dtos.Requests.Category;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface ICategoriesService
{
    public Task CreateCategoryAsync(CreateCategoryRequest request);
    public Task<IEnumerable<string>> GetAllCategoriesAsync(int count);
    public Task<Category> GetCategoryByIdAsync(Guid id);
    public Task DeleteByIdCategoryAsync(Guid id);
}