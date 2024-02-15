using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Category;
using Application.Exceptions;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

public class CategoriesService(ICategoriesRepository _categoriesRepository, IMapper mapper) : ICategoriesService
{
    public async Task<Category> CreateCategoryAsync(CreateCategoryRequestDto requestDto, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetCategoryByNameAsync(requestDto.Name, cancellationToken);

        if (category is not null)
        {
            throw new BadRequestException("Category with this name already exists");
        }

        var categoryModel = new Category
        {
            Id = Guid.NewGuid(),
            Name = requestDto.Name
        };
            
        await _categoriesRepository.AddAsync(categoryModel, cancellationToken: cancellationToken );
        
        return categoryModel;
    }
    

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository
            .GetAllAsync(pageSettingsRequestDto, cancellationToken);
        
        return categories.ToList();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await _categoriesRepository.GetByIdAsync(id, cancellationToken) is not Category category)
        {
            throw new NotFoundException("Category doesn't exist");
        }

        return category;
    }

    public async Task DeleteByIdCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await _categoriesRepository.IsExistsAsync(id, cancellationToken))
        {
            throw new NotFoundException("Category not found");
        }

        await _categoriesRepository.DeleteByIdAsync(id, cancellationToken);
    }
}