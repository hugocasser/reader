using BusinessLogicLayer.Abstractions.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Attributes;

namespace PresentationLayer.Abstractions;

[ApiController]
[Produces("application/json")]
[RequestDataValidation]
public abstract class ApiController(IUsersService usersService) : ControllerBase
{
    protected readonly IUsersService _usersService = usersService;
}