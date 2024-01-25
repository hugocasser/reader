using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Abstractions;

namespace PresentationLayer.Controllers;

[Route("api/identity/users")]
public sealed class UsersController : ApiController
{
    public UsersController(IUsersService usersService) : base(usersService)
    { }

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