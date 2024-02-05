using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Abstractions;

namespace PresentationLayer.Controllers;


[Route("api/identity/roles")]
public class RolesController(IRolesService rolesService, IUsersService usersService) : ApiController(usersService)
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string role)
    {
        return Ok(await rolesService.CreateRoleAsync(role));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{role}")]
    public async Task<IActionResult> DeleteRoleAsync(string role)
    {
        return Ok(await rolesService.DeleteRoleAsync(role));
    }
    
    [HttpGet("{role}")]
    public async Task<IActionResult> GetRoleInfoAsync(string role)
    {
        return Ok(await rolesService.GetRoleInfoAsync(role));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> GiveRoleToUserAsync([FromBody]GiveRoleToUserRequestDto giveRoleToUserRequestDto)
    {
        return Ok(await _usersService.GiveRoleToUserAsync(giveRoleToUserRequestDto));
    }
}