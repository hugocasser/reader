using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("api/books")]
public class BooksController(IBooksService booksService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks(PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllBooksAsync(pageSettings, cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetBookByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("{id}/info")]
    public async Task<IActionResult> GetBookInfoByIdAsync
        (Guid id, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetBookInfoByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("categories/{categoryId}/books")]
    public async Task<IActionResult> GetBooksByCategoryAsync
        (Guid categoryId, PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllCategoryBooksAsync(categoryId, pageSettings, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync
        (CreateBookRequest createBookRequest, CancellationToken cancellationToken)
    {
        await booksService.CreateBookAsync(createBookRequest, cancellationToken);
        return Created();
    }

    [HttpPut("info")]
    public async Task<IActionResult> UpdateBookInfoAsync
        (UpdateBookInfoRequest updateBookInfoRequest, CancellationToken cancellationToken)
    {
        await booksService.UpdateBookInfoAsync(updateBookInfoRequest, cancellationToken);
        return Ok($"Book information with id {updateBookInfoRequest.Id} updated");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBookTextAsync
        (UpdateBookTextRequest updateBookTextRequest, CancellationToken cancellationToken)
    {
        await booksService.UpdateBookTextAsync(updateBookTextRequest, cancellationToken);
        return Ok($"Book text with id {updateBookTextRequest.Id} updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync
        (Guid id, CancellationToken cancellationToken)
    {
        await booksService.DeleteByIdBookAsync(id, cancellationToken);
        return Ok($"Book with id {id} deleted");
    }
    
}