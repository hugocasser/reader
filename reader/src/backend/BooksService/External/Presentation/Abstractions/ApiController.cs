using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Abstractions;

[ApiController]
[Produces("application/json")]
[RequestDataValidation]
public abstract class ApiController : ControllerBase
{
    
}