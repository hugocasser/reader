using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/identity/auth")]
public class AuthController : ControllerBase
{
    private readonly IUsersService _usersService;

    public AuthController(IUsersService usersService)
    {
        _usersService = usersService;
    }

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