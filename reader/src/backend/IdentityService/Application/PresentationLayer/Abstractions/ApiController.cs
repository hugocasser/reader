using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Attributes;

namespace PresentationLayer.Abstractions;

[ApiController]
[Produces("application/json")]
[RequestDataValidation]
public abstract class ApiController : ControllerBase
{
    protected readonly IUsersService _usersService;

    protected ApiController(IUsersService usersService)
    {
        _usersService = usersService;
    }
}