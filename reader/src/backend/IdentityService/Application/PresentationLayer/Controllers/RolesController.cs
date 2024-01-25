using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[Produces("application/json")]
[Route("api/identity/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRolesService _rolesService;
    private readonly IUsersService _usersService;

    public RolesController(IRolesService rolesService, IUsersService usersService)
    {
        _rolesService = rolesService;
        _usersService = usersService;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string role)
    {
        return Ok(await _rolesService.CreateRoleAsync(role));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{role}")]
    public async Task<IActionResult> DeleteRoleAsync(string role)
    {
        return Ok(await _rolesService.DeleteRoleAsync(role));
    }
    
    [HttpGet("{role}")]
    public async Task<IActionResult> GetRoleInfoAsync(string role)
    {
        return Ok(await _rolesService.GetRoleInfoAsync(role));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto, 
        CancellationToken cancellationToken)
    {
        return Ok(await _usersService.GiveRoleToUserAsync(giveRoleToUserRequestDto, cancellationToken));
    }
}