using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("api/authors")]
public class AuthorsController(IAuthorsService _authorsService, IBooksService _booksService) : ApiController
{
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetAllAuthors(int page, int size, CancellationToken cancellationToken)
    {
        var pageSettings = new PageSettingRequestDto(page, size);
        
        return Ok(await _authorsService.GetAllAuthorsAsync(pageSettings, cancellationToken));
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _authorsService.GetAuthorByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("{id}/books")]
    [Authorize]
    public async Task<IActionResult> GetBooksByAuthorAsync
        (int page, int size, Guid id, CancellationToken cancellationToken)
    {
        var pageSettingsRequestDto = new PageSettingRequestDto(page, size);
        
        return Ok(await _booksService.GetAllAuthorBooksAsync(id, pageSettingsRequestDto, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> CreateAuthorAsync
        ([FromBody] CreateAuthorRequestDto createAuthorRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _authorsService.CreateAuthorAsync(createAuthorRequestDto, cancellationToken));
    }

    [HttpPut]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> UpdateAuthorAsync
        ([FromBody] UpdateAuthorRequestDto updateAuthorRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _authorsService.UpdateAuthorAsync(updateAuthorRequestDto, cancellationToken));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        await _authorsService.DeleteByIdAuthorAsync(id, cancellationToken);
        
        return NoContent();
    }
}