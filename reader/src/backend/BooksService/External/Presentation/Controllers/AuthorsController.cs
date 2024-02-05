using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[Route("api/authors")]
public class AuthorsController(IAuthorsService authorsService, IBooksService booksService) : ApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAuthors([FromBody]PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await authorsService.GetAllAuthorsAsync(pageSettings, cancellationToken));
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await authorsService.GetAuthorByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("{id}/books")]
    [Authorize]
    public async Task<IActionResult> GetBooksByAuthorAsync
        (Guid id, PageSetting pageSettings, CancellationToken cancellationToken)
    {
        return Ok(await booksService.GetAllAuthorBooksAsync(id, pageSettings, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> CreateAuthorAsync
        (CreateAuthorRequest createAuthorRequest, CancellationToken cancellationToken)
    {
        await authorsService.CreateAuthorAsync(createAuthorRequest, cancellationToken);

        return Created();
    }

    [HttpPut]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> UpdateAuthorAsync
        (UpdateAuthorRequest updateAuthorRequest, CancellationToken cancellationToken)
    {
        await authorsService.UpdateAuthorAsync(updateAuthorRequest, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        await authorsService.DeleteByIdAuthorAsync(id, cancellationToken);
        
        return NoContent();
    }
}