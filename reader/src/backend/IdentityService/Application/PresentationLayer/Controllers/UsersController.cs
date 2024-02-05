using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PresentationLayer.Abstractions;

namespace PresentationLayer.Controllers;

[Route("api/identity/users")]
public sealed class UsersController(IUsersService usersService) : ApiController(usersService)
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return Ok(await _usersService.GetAllUsersAsync(page, pageSize, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        return Ok(await _usersService.GetUserByIdAsync(id));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserByIdAsync(Guid id)
    {
        await _usersService.DeleteUserByIdAsync(id);
        
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody]UpdateUserRequestDto userRequestViewDto,
        CancellationToken cancellationToken)
    {
        await _usersService.UpdateUserAsync(userRequestViewDto, cancellationToken);

        return NoContent();
    }
}