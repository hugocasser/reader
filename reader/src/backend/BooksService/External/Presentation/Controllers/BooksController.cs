using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;


namespace Presentation.Controllers;

[Route("api/books")]
public class BooksController(IBooksService _booksService) : ApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllBooks(int page, int size, CancellationToken cancellationToken)
    {
        var pageSettingsRequestDto = new PageSettingRequestDto(page, size);
        
        return Ok(await _booksService.GetAllBooksAsync(pageSettingsRequestDto, cancellationToken));
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _booksService.GetBookByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("{id}/info")]
    [Authorize]
    public async Task<IActionResult> GetBookInfoByIdAsync
        (Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _booksService.GetBookInfoByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("categories/{categoryId}/books")]
    [Authorize]
    public async Task<IActionResult> GetBooksByCategoryAsync
        (Guid categoryId, int page, int size, CancellationToken cancellationToken)
    {
        var pageSettings = new PageSettingRequestDto(page, size);
        
        return Ok(await _booksService.GetAllCategoryBooksAsync(categoryId, pageSettings, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> CreateBookAsync
        ([FromBody]CreateBookRequestDto createBookRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _booksService.CreateBookAsync(createBookRequestDto, cancellationToken));
    }

    [HttpPut("info")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> UpdateBookInfoAsync
        ([FromBody]UpdateBookInfoRequestDto updateBookInfoRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _booksService.UpdateBookInfoAsync(updateBookInfoRequestDto, cancellationToken));
    }

    [HttpPut]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> UpdateBookTextAsync
        ([FromBody]UpdateBookTextRequestDto updateBookTextRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _booksService.UpdateBookTextAsync(updateBookTextRequestDto, cancellationToken));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> DeleteBookAsync(Guid id, CancellationToken cancellationToken)
    {
        await _booksService.DeleteByIdBookAsync(id, cancellationToken);

        return NoContent();
    }
    
}