using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Abstractions;
using PresentationLayer.Attributes;

namespace PresentationLayer.Controllers;

[Route("api/identity/auth")]
public class AuthController(IUsersService usersService, IRefreshTokensService _refreshTokensService)
    : ApiController(usersService)
{

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(LoginUserRequestDto loginRequestDto,
        CancellationToken cancellationToken)
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
        return Ok(await _usersService.ResendEmailConfirmMessageAsync(email, password, cancellationToken));
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync
        ([FromBody]UpdateAuthTokenRequestDto updateAuthTokenRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _refreshTokensService.UpdateAuthTokenAsync(cancellationToken, updateAuthTokenRequestDto));
    }
}