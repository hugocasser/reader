using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("api/authors")]
public class AuthorsController(IAuthorsService authorsService, IBooksService booksService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors([FromBody]PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await authorsService.GetAllAuthorsAsync(pageSettings, cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await authorsService.GetAuthorByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("{id}/books")]
    public async Task<IActionResult> GetBooksByAuthorAsync
        (Guid id, PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllAuthorBooksAsync(id, pageSettings, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync
        (CreateAuthorRequest createAuthorRequest, CancellationToken cancellationToken)
    {
        await authorsService.CreateAuthorAsync(createAuthorRequest, cancellationToken);
        return Ok("Author Created");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthorAsync
        (UpdateAuthorRequest updateAuthorRequest, CancellationToken cancellationToken)
    {
        await authorsService.UpdateAuthorAsync(updateAuthorRequest, cancellationToken);
        return Ok($"Author with id {updateAuthorRequest.Id} updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        await authorsService.DeleteByIdAuthorAsync(id, cancellationToken);
        return Ok($"Author with id {id} deleted");
    }
}