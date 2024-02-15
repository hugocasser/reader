using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Abstractions;

namespace PresentationLayer.Controllers;

[Route("api/identity/roles")]
public class RolesController(IRolesService _rolesService, IUsersService _usersService) : ApiController(_usersService)
{
    [Authorize(Roles = nameof(EnumRoles.Admin))]
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string role)
    {
        return Ok(await _rolesService.CreateRoleAsync(role));
    }
    
    [Authorize(Roles = nameof(EnumRoles.Admin))]
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

    [Authorize(Roles = nameof(EnumRoles.Admin))]
    [HttpPut]
    public async Task<IActionResult> GiveRoleToUserAsync([FromBody]GiveRoleToUserRequestDto giveRoleToUserRequestDto)
    {
        return Ok(await _usersService.GiveRoleToUserAsync(giveRoleToUserRequestDto));
    }
}