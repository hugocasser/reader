using Application.Dtos.Requests;
using Application.Dtos.Requests.Category;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface ICategoriesService
{
    public Task<Category> CreateCategoryAsync(CreateCategoryRequestDto requestDto, CancellationToken cancellationToken);
    public Task<IEnumerable<Category>> GetAllCategoriesAsync(PageSettingRequestDto pageSettingsRequestDto,
        CancellationToken cancellationToken);
    public Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken) ;
    public Task DeleteByIdCategoryAsync(Guid id, CancellationToken cancellationToken);
}