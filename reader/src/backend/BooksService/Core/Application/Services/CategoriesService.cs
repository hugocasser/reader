using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Requests.Category;
using Application.Exceptions;
using Domain.Models;

namespace Application.Services;

public class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
{
    public async Task CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetCategoryByNameAsync(request.Name, cancellationToken);

        if (category is not null)
        {
            throw new BadRequestExceptionWithStatusCode("Category with this name already exists");
        }

        await categoriesRepository.AddCategoryAsync(new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        }, cancellationToken: cancellationToken );
    }
    

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(PageSettings pageSettings, CancellationToken cancellationToken)
    {
        var categories = await categoriesRepository
            .GetCategoriesAsync(pageSettings.PageSize,
                pageSettings.PageSize*(pageSettings.PageNumber-1), cancellationToken);
        
        return categories.ToList();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetCategoryByIdAsync(id, cancellationToken);

        if (category is null)
        {
            throw new NotFoundExceptionWithStatusCode("Category doesn't exist");
        }

        return category;
    }

    public async Task DeleteByIdCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await categoriesRepository.CategoryExistsAsync(id, cancellationToken))
        {
            throw new NotFoundExceptionWithStatusCode("Category not found");
        }

        await categoriesRepository.DeleteByIdCategoryAsync(id, cancellationToken);
    }
}