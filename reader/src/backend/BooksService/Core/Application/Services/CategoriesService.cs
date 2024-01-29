using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos.Requests.Category;
using Application.Exceptions;
using Domain.Models;

namespace Application.Services;

public class CategoriesService(ICategoryRepository categoryRepository) : ICategoriesService
{
    public async Task CreateCategoryAsync(CreateCategoryRequest request)
    {
        var category = await categoryRepository.GetCategoryByNameAsync(request.Name);

        if (category is not null)
        {
            throw new BadRequestExceptionWithStatusCode("Category with this name already exists");
        }

        await categoryRepository.AddCategoryAsync(new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        });
    }
    

    public async Task<IEnumerable<string>> GetAllCategoriesAsync(int count)
    {
        var categories = await categoryRepository.GetCategoriesAsync();
        
        return categories.OrderBy(c => c.Name).Take(count).Select(category => category.Name).ToList();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(id);

        if (category is null)
        {
            throw new NotFoundExceptionWithStatusCode("Category doesn't exist");
        }

        return category;
    }

    public async Task DeleteByIdCategoryAsync(Guid id)
    {
        if (!await categoryRepository.CategoryExistsAsync(id))
        {
            throw new NotFoundExceptionWithStatusCode("Category not found");
        }

        await categoryRepository.DeleteByIdCategoryAsync(id);
    }
}