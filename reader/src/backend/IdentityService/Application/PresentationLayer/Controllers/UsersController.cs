using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/identity/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return Ok(await _usersService.GetAllUsersAsync(cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _usersService.GetUserByIdAsync(id, cancellationToken));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _usersService.DeleteUserByIdAsync(id, cancellationToken);
        return Ok($"User({id}) deleted");
    }
    
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserRequestDto userRequestViewDto,
        CancellationToken cancellationToken)
    {
        await _usersService.UpdateUserAsync(userRequestViewDto, cancellationToken);
        return Ok($"User({userRequestViewDto.OldEmail}) updated");
    }
    
}