using Application.Common;
using Application.Dtos.Requests.Category;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface ICategoriesService
{
    public Task CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<Category>> GetAllCategoriesAsync(PageSettings pageSettings,
        CancellationToken cancellationToken);
    public Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken) ;
    public Task DeleteByIdCategoryAsync(Guid id, CancellationToken cancellationToken);
}