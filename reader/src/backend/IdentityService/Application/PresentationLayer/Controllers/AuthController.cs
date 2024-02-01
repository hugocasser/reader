using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Abstractions;

namespace PresentationLayer.Controllers;

[Route("api/identity/auth")]
public class AuthController(IUsersService usersService, IRefreshTokensService refreshTokensService)
    : ApiController(usersService)
{

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginUserRequestDto loginRequestDto,
        CancellationToken cancellationToken)
    {
        return Ok(await _usersService.LoginUserAsync(loginRequestDto, cancellationToken));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserRequestDto registerUserRequestDto,
        CancellationToken cancellationToken)
    {
        await _usersService.RegisterUserAsync(registerUserRequestDto, cancellationToken);
        
        return Created();
    }

    [HttpGet]
    [Route("{userId}/{code}")]
    public async Task<IActionResult> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken)
    {
        return Ok(await _usersService.ConfirmUserEmail(new Guid(userId), code));
    }

    [HttpPost]
    [Route("resend")]
    public async Task<IActionResult> ResendEmailConfirmMessageAsync(string email, string password, CancellationToken cancellationToken)
    {
        return Ok(await _usersService.ResendEmailConfirmMessageAsync(email, password));
    }

    [HttpPost]
    [Route("refreshtoken")]
    public async Task<IActionResult> RefreshTokenAsync
        ([FromBody]UpdateAuthTokenRequestDto updateAuthTokenRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await refreshTokensService.RefreshTokenAsync(updateAuthTokenRequestDto, cancellationToken));
    }
}