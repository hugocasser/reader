using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("api/categories")]
public class CategoriesController(ICategoriesService _categoriesService, IBooksService _booksService) : ApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCategories([FromQuery]int page, [FromQuery]int size, CancellationToken cancellationToken)
    {
        var pageSettings = new PageSettingRequestDto(page, size);
        
        return Ok(await _categoriesService.GetAllCategoriesAsync(pageSettings, cancellationToken));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _categoriesService.GetCategoryByIdAsync(id, cancellationToken));
    }

    [HttpGet("{id}/books")]
    [Authorize]
    public async Task<IActionResult> GetBooksByCategoryAsync
        (Guid id, int page, int size, CancellationToken cancellationToken)
    {
        var pageSettings = new PageSettingRequestDto(page, size);
        
        return Ok(await _booksService.GetAllCategoryBooksAsync(id, pageSettings, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> CreateCategoryAsync
        ([FromBody]CreateCategoryRequestDto request, CancellationToken cancellationToken)
    {
        await _categoriesService.CreateCategoryAsync(request, cancellationToken);
        
        return Created();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoriesService.DeleteByIdCategoryAsync(id, cancellationToken);
        
        return NoContent();
    }
}