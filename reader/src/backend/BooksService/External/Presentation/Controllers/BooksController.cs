using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[Route("api/books")]
public class BooksController(IBooksService booksService) : ApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllBooks(PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllBooksAsync(pageSettings, cancellationToken));
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetBookByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("{id}/info")]
    [Authorize]
    public async Task<IActionResult> GetBookInfoByIdAsync
        (Guid id, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetBookInfoByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("categories/{categoryId}/books")]
    [Authorize]
    public async Task<IActionResult> GetBooksByCategoryAsync
        (Guid categoryId, PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllCategoryBooksAsync(categoryId, pageSettings, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> CreateBookAsync
        (CreateBookRequest createBookRequest, CancellationToken cancellationToken)
    {
        await booksService.CreateBookAsync(createBookRequest, cancellationToken);
        
        return Created();
    }

    [HttpPut("info")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> UpdateBookInfoAsync
        (UpdateBookInfoRequest updateBookInfoRequest, CancellationToken cancellationToken)
    {
        await booksService.UpdateBookInfoAsync(updateBookInfoRequest, cancellationToken);

        return NoContent();
    }

    [HttpPut]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> UpdateBookTextAsync
        (UpdateBookTextRequest updateBookTextRequest, CancellationToken cancellationToken)
    {
        await booksService.UpdateBookTextAsync(updateBookTextRequest, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> DeleteBookAsync
        (Guid id, CancellationToken cancellationToken)
    {
        await booksService.DeleteByIdBookAsync(id, cancellationToken);

        return NoContent();
    }
    
}