using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[Route("api/categories")]
public class CategoriesController(ICategoriesService categoriesService, IBooksService booksService) : ApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCategories(PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await categoriesService.GetAllCategoriesAsync(pageSettings, cancellationToken));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await categoriesService.GetCategoryByIdAsync(id, cancellationToken));
    }

    [HttpGet("{id}/books")]
    [Authorize]
    public async Task<IActionResult> GetBooksByCategoryAsync
        (Guid id, PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllCategoryBooksAsync(id, pageSettings, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> CreateCategoryAsync
        (CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        await categoriesService.CreateCategoryAsync(request, cancellationToken);
        
        return Created();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        await categoriesService.DeleteByIdCategoryAsync(id, cancellationToken);
        
        return NoContent();
    }
}