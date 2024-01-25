using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Abstractions;
using PresentationLayer.Attributes;

namespace PresentationLayer.Controllers;


[Route("api/identity/auth")]
public class AuthController(IUsersService usersService) : ApiController(usersService)
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(LoginUserRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _usersService.LoginUserAsync(loginRequestDto, cancellationToken));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserRequestDto registerUserRequestDto,
        CancellationToken cancellationToken)
    {
        await _usersService.RegisterUserAsync(registerUserRequestDto, cancellationToken);
        return Ok();
    }
}